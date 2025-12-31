using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using Confluent.Kafka;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace TwistedTaleweaver.Core.Kafka;

public class KafkaSubscriber<T> : BackgroundService where T : class
{
    private readonly IConsumer<string, string> _consumer;
    private readonly ILogger<KafkaSubscriber<T>> _logger;
    private readonly IKafkaEventDispatcher _dispatcher;

    private readonly JsonSerializerOptions _serializerOptions = new()
    {
        Converters = { new JsonStringEnumConverter() }
    };
    
    public KafkaSubscriber(
        IKafkaEventDispatcher dispatcher,
        ILogger<KafkaSubscriber<T>> logger, 
        IConfiguration configuration)
    {
        _dispatcher = dispatcher;
        _logger = logger;

        var connectionString = configuration.GetConnectionString("Kafka")
                               ?? throw new InvalidOperationException("Kafka ConnectionString is not configured.");

        var groupId = configuration["Kafka:GroupId"] ??  throw new InvalidOperationException("Kafka GroupId is not configured.");
        
        var config = new ConsumerConfig
        {
            BootstrapServers = connectionString,
            GroupId = groupId,
            AutoOffsetReset = AutoOffsetReset.Earliest,
            EnableAutoCommit = false
        };

        var topic = typeof(T).FullName!;
        _consumer = new ConsumerBuilder<string, string>(config).Build();
        _consumer.Subscribe(topic);
        
        _logger.LogInformation("Kafka subscriber for {EventType} initialized and subscribed to topic {Topic} for GroupId {GroupId}.", typeof(T).FullName, topic, groupId);
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Kafka subscriber for {EventType} started.", typeof(T).FullName);
        
        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                var result = _consumer.Consume(stoppingToken);
                
                var headers = result.Message.Headers;
                if (!headers.TryGetLastBytes(KafkaConstants.EventTypeHeader, out var typeBytes))
                {
                    throw new InvalidOperationException("Kafka message is missing EventType header.");
                }
                
                var headerTypeName = Encoding.UTF8.GetString(typeBytes);

                var eventType = typeof(T);
                if (eventType.FullName != headerTypeName)
                {
                    throw new InvalidOperationException($"Kafka message EventType header '{headerTypeName}' does not match expected type '{eventType.FullName}'.");
                }
                
                var @event = JsonSerializer.Deserialize(result.Message.Value, eventType, _serializerOptions);
                if (@event == null)
                {
                    throw new InvalidOperationException($"Failed to deserialize event of type: {eventType.FullName}");
                }
                
                await _dispatcher.DispatchAsync(@event, stoppingToken);

                _consumer.Commit(result);
            }
            catch (ConsumeException e)
            {
               _logger.LogError(e, "Error consuming Kafka message: {ErrorReason}", e.Error.Reason);
            }
        }
    }
}
