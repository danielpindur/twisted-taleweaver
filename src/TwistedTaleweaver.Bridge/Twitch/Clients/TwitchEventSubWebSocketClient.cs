using System.Net.WebSockets;
using System.Text;
using System.Text.Json;
using TwistedTaleweaver.Bridge.Twitch.Common.Deserialization;
using TwistedTaleweaver.Bridge.Twitch.Common.Models;
using TwistedTaleweaver.Bridge.Twitch.Common.Models.Enums;
using TwistedTaleweaver.Bridge.Twitch.Common.Processing;
using TwistedTaleweaver.Bridge.Twitch.Configuration;
using TwistedTaleweaver.Bridge.Twitch.Subscriptions;
using Microsoft.Extensions.Options;

namespace TwistedTaleweaver.Bridge.Twitch.Clients;

internal class TwitchEventSubWebSocketClient : BackgroundService
{
    private const int BufferSize = 16 * 1024; // 16KB buffer
    private const int CriticalFailureWaitSeconds = 1;
    private const int PingIntervalSeconds = 30;
    private const int DefaultKeepaliveTimeoutSeconds = 10;
    private const double KeepaliveWaitOffsetSeconds = .5;
    
    private readonly ILogger<TwitchEventSubWebSocketClient> _logger;
    private readonly TwitchWebsocketConfiguration _websocketConfiguration;
    private readonly INotificationProcessorFactory _notificationProcessorFactory;
    private readonly ITwitchEventSubscriptionCreator _twitchEventSubscriptionCreator;
    
    private string? _sessionId;
    private ClientWebSocket? _activeWebSocket;
    private int? _keepAliveTimeoutSeconds;
    
    // TODO: This needs to be reworked to load the broadcasters from DB and crete/remove subscriptions adhoc
    public TwitchEventSubWebSocketClient(
        ILogger<TwitchEventSubWebSocketClient> logger,
        IOptions<TwitchWebsocketConfiguration> configuration,
        INotificationProcessorFactory notificationProcessorFactory, 
        ITwitchEventSubscriptionCreator twitchEventSubscriptionCreator) 
    {
        _logger = logger;
        _notificationProcessorFactory = notificationProcessorFactory;
        _twitchEventSubscriptionCreator = twitchEventSubscriptionCreator;
        _websocketConfiguration = configuration.Value;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                await ConnectAndListenAsync(stoppingToken);
            }
            catch (OperationCanceledException)
            {
                // Graceful shutdown.
                _logger.LogWarning("Cancellation requested, shutting down ...");
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex, $"Error in WebSocket connection. Retrying in {CriticalFailureWaitSeconds} seconds...");
                
                // TODO: This delay is too long, we need to figure out something else
                await Task.Delay(TimeSpan.FromSeconds(CriticalFailureWaitSeconds), stoppingToken);
            }
        }
    }

    private async Task ConnectAndListenAsync(CancellationToken stoppingToken)
    {
        // TryFinally instead of using as we need to be able to change _activeWebSocket inside
        try
        {
            _activeWebSocket = await ConnectAsync(_websocketConfiguration.EventSubWebSocketUrl, stoppingToken);
            await ReceiveMessagesAsync(stoppingToken);
        }
        catch (Exception)
        {
            // Exception was thrown during processing, we need to close the connection if open
            if (_activeWebSocket?.State == WebSocketState.Open)
            {
                await TryCloseConnectionAsync(_activeWebSocket, WebSocketCloseStatus.InternalServerError, stoppingToken);
            }
            
            throw;
        }
        finally
        {
            // Revocation message received, we need to close the connection if open
            if (_activeWebSocket?.State == WebSocketState.Open)
            {
                await TryCloseConnectionAsync(_activeWebSocket, WebSocketCloseStatus.NormalClosure, stoppingToken);
            }
            
            _activeWebSocket?.Dispose();
            _activeWebSocket = null;
        }
    }

    private async Task<ClientWebSocket> ConnectAsync(string connectionUrl, CancellationToken stoppingToken)
    {
        ClientWebSocket? webSocket = null;
        var connectionUri = new Uri(connectionUrl);

        try
        {
            webSocket = new ClientWebSocket();
            webSocket.Options.KeepAliveInterval = TimeSpan.FromSeconds(PingIntervalSeconds);
            
            await webSocket.ConnectAsync(connectionUri, stoppingToken);
        }
        catch (Exception)
        {
            // Failed to connect, no need to close connection
            webSocket?.Dispose();
            throw;
        }
        
        _logger.LogInformation($"Connected to Twitch EventSub WebSocket at {connectionUri}");

        return webSocket;
    }

    private async Task ReceiveMessagesAsync(CancellationToken stoppingToken)
    {
        while (_activeWebSocket!.State == WebSocketState.Open && !stoppingToken.IsCancellationRequested)
        {
            using var keepAliveTimeOutCts = CancellationTokenSource.CreateLinkedTokenSource(stoppingToken);
            var keepAliveTimeoutSeconds = (_keepAliveTimeoutSeconds ?? DefaultKeepaliveTimeoutSeconds) + KeepaliveWaitOffsetSeconds;
            keepAliveTimeOutCts.CancelAfter(TimeSpan.FromSeconds(keepAliveTimeoutSeconds));

            string message;
            
            try
            {
                message = await ReceiveMessageAsync(_activeWebSocket, keepAliveTimeOutCts.Token);

            }
            catch (OperationCanceledException)
            {
                if (!stoppingToken.IsCancellationRequested && keepAliveTimeOutCts.IsCancellationRequested)
                {
                    // We timed-out because of no keep-alive notification - the connection is dead, reconnect 
                    _logger.LogWarning(
                        $"Haven't received KeepAlive message in {keepAliveTimeoutSeconds} seconds, initiating reconnect.");
                    
                    // ReSharper disable once PossiblyMistakenUseOfCancellationToken - we want to use the stopping token
                    await ReconnectAsync(_websocketConfiguration.EventSubWebSocketUrl, stoppingToken);
                    return;
                }

                throw;
            }
            
            // ReSharper disable once PossiblyMistakenUseOfCancellationToken - we want to use the stopping token
            await HandleMessageAsync(message, stoppingToken);
        }

        if (_activeWebSocket!.State != WebSocketState.Open)
        {
            _logger.LogWarning("WebSocket connection closed, WebSocketState: {WebSocketState}", _activeWebSocket.State);
        }
    }

    private async Task<string> ReceiveMessageAsync(ClientWebSocket webSocket, CancellationToken stoppingToken)
    {
        var buffer = new byte[BufferSize];
        var receiveBuffer = new ArraySegment<byte>(buffer);
        var messageBuilder = new StringBuilder();
        
        WebSocketReceiveResult result;

        do
        {
            result = await webSocket.ReceiveAsync(receiveBuffer, stoppingToken);
            var messageChunk = Encoding.UTF8.GetString(buffer, 0, result.Count);
            messageBuilder.Append(messageChunk);
        } while (!result.EndOfMessage);

        return messageBuilder.ToString();
    }

    private async Task HandleMessageAsync(string message, CancellationToken stoppingToken)
    {
        try
        {
            var notification = TransformMessage(message);

            if (notification is null || notification.Metadata.MessageType == MessageType.Keepalive)
            {
                return;
            } 
            
            switch (notification.Metadata.MessageType)
            {
                case MessageType.Welcome:
                    await HandleWelcomeMessageAsync(notification, true);
                    break;
                    
                case MessageType.Notification:
                    await HandleNotificationMessageAsync(notification);
                    break;
                    
                case MessageType.Reconnect:
                    await HandleReconnectMessageAsync(notification, stoppingToken);
                    break;
                
                case MessageType.Revocation:
                    _logger.LogError("Received revocation message from Twitch: {Message}", message);
                    break;
                    
                default:
                    _logger.LogWarning("Unknown message type: {MessageType}", notification.Metadata.MessageType);
                    break;
            }
        }
        catch (JsonException ex)
        {
            _logger.LogError(ex, "Error deserializing message: {Message}", message);
        }
    }

    private TwitchEventSubNotification? TransformMessage(string message)
    {
        if (string.IsNullOrWhiteSpace(message))
        {
            return null;
        }
            
        var notification = JsonSerializer.Deserialize<TwitchEventSubNotification>(message, TwitchWebsocketJsonDeserialization.Options);
            
        if (notification is null)
        {
            _logger.LogWarning("Received null notification from Twitch");
            return null;
        }

        if (notification.Metadata.MessageType == MessageType.Keepalive)
        {
            // We receive these every 10 seconds, don't log it
            return notification;
        }
            
        _logger.LogInformation("{@TwitchEventSubNotification}", notification);
        
        return notification;
    }

    private async Task HandleWelcomeMessageAsync(TwitchEventSubNotification notification, bool createSubscriptions)
    {
        _sessionId = notification.Payload.Session?.SessionId;
        _keepAliveTimeoutSeconds = notification.Payload.Session?.KeepaliveTimeoutSeconds;

        if (createSubscriptions)
        {
            await _twitchEventSubscriptionCreator.Create(_sessionId!);
        }
    }

    private async Task HandleReconnectMessageAsync(TwitchEventSubNotification notification, CancellationToken stoppingToken)
    {
        // We need to connect to the new websocket and disconnect from the existing one, only after receiving Welcome from new websocket
        // The new websocket has cloned all the existing subscriptions, so we don't want to send subscription API calls

        var reconnectUrl = notification.Payload.Session?.ReconnectUrl ??
                           throw new ArgumentNullException(nameof(notification.Payload.Session.ReconnectUrl));
        
        await ReconnectAsync(reconnectUrl, stoppingToken);
    }
    
    private async Task ReconnectAsync(string connectionUrl, CancellationToken stoppingToken)
    {
        // We need to connect to the new websocket and disconnect from the existing one, only after receiving Welcome from new websocket
        
        var isReconnectMessage = connectionUrl != _websocketConfiguration.EventSubWebSocketUrl;
        
        ClientWebSocket? newWebSocket = null;

        try
        {
            // Create new connection
            newWebSocket = await ConnectAsync(connectionUrl, stoppingToken);
        }
        catch (Exception)
        {
            newWebSocket?.Dispose();
            throw;
        }

        try
        {
            var timeoutCts = CancellationTokenSource.CreateLinkedTokenSource(stoppingToken);
            timeoutCts.CancelAfter(TimeSpan.FromSeconds(30));
            
            // Wait for welcome message
            var receivedMessage = await ReceiveMessageAsync(newWebSocket, timeoutCts.Token);
            var receivedNotification = TransformMessage(receivedMessage);

            if (receivedNotification is null || receivedNotification.Metadata.MessageType != MessageType.Welcome)
            {
                _logger.LogError("Received unexpected notification while trying to reconnect");
                throw new Exception("Received unexpected notification while trying to reconnect");
            }
            
            await HandleWelcomeMessageAsync(receivedNotification, !isReconnectMessage);
        }
        catch (Exception exception)
        {
            if (exception is OperationCanceledException)
            {
                _logger.LogError("Operation timeout while waiting for welcome message");
            }
            
            await TryCloseConnectionAsync(newWebSocket, WebSocketCloseStatus.InternalServerError, stoppingToken);
            newWebSocket.Dispose();
            throw;
        }
        
        // Close existing connection and replace with new one
        if (_activeWebSocket is not null)
        {
            var closureReason = isReconnectMessage
                ? "Disconnected from Twitch EventSub WebSocket due to ReconnectMessage received"
                : "Disconnected from Twitch EventSub WebSocket";
            
            await TryCloseConnectionAsync(
                _activeWebSocket,
                WebSocketCloseStatus.NormalClosure,
                stoppingToken,
                closureReason);
            _activeWebSocket.Dispose();
            _activeWebSocket = null;
            
            _logger.LogInformation(closureReason);
        }

        _activeWebSocket = newWebSocket;
    }

    private async Task HandleNotificationMessageAsync(TwitchEventSubNotification notification)
    {
        // TODO: Handle replay attacks - message_timestamp is not older then 10 minutes, haven't received given message_id before
        
        if (notification.Payload.Event is null)
        {
            _logger.LogError("Received notification with null event payload");
            return;
        }
        
        if (notification.Metadata.SubscriptionType is null)
        {
            _logger.LogWarning("Received notification with null or not implemented subscription type");
            throw new JsonException(); // To log the message via the encapsulating try-catch
        }

        var processor = _notificationProcessorFactory.Create(notification.Metadata.SubscriptionType!.Value);
        await processor.ProcessAsync(new TwitchNotification(notification));
    }

    private async Task TryCloseConnectionAsync(
        WebSocket socket,
        WebSocketCloseStatus closeStatus,
        CancellationToken cancellationToken,
        string? reason = null)
    {
        if (socket.State != WebSocketState.Open)
        {
            return;
        }
        
        try
        {
            await socket.CloseAsync(
                closeStatus, 
                reason ?? string.Empty, 
                cancellationToken);
        }
        catch (Exception e)
        {
            _logger.LogWarning(e, "Error closing WebSocket after exception");
        }
    }
}
