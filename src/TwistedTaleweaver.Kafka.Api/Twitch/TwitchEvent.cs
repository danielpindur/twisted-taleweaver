using TwistedTaleweaver.Core.Kafka;
using TwistedTaleweaver.Kafka.Api.Twitch.Payloads.Chat;
using TwistedTaleweaver.Kafka.Api.Twitch.Payloads.Stream;

namespace TwistedTaleweaver.Kafka.Api.Twitch;

/// <summary>
/// Represents the events related to Twitch notifications.
/// </summary>
public class TwitchEvent(string eventType, string topic, Type payloadType) : KafkaEvent(eventType, topic, payloadType)
{
    private const string StreamEventsTopic = "twitch-stream-events";
    private const string ChatEventsTopic = "twitch-chat-events";

    /// <summary>
    /// Fired when a user starts streaming
    /// </summary>
    public static readonly TwitchEvent StreamStarted = new(nameof(StreamStarted), StreamEventsTopic, typeof(StreamStartedPayload));
    
    /// <summary>
    /// Fired when user ends a stream
    /// </summary>
    public static readonly TwitchEvent StreamEnded = new(nameof(StreamEnded), StreamEventsTopic, typeof(StreamEndedPayload));
    
    /// <summary>
    /// Fired when a user sends a message in chat
    /// </summary>
    public static readonly TwitchEvent ChatMessage = new(nameof(ChatMessage), ChatEventsTopic, typeof(ChatMessagePayload));
}