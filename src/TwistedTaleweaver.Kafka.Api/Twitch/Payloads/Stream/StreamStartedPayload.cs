using TwistedTaleweaver.Core.Kafka;
using TwistedTaleweaver.Core.Kafka.Attributes;

namespace TwistedTaleweaver.Kafka.Api.Twitch.Payloads.Stream;

/// <summary>
/// Represents a stream started payload for Twitch EventSub notifications.
/// </summary>
public class StreamStartedPayload : EventPayload
{
    /// <summary>
    /// Unique identifier for the user broadcaster
    /// </summary>
    [PartitionKey]
    public required string BroadcasterUserId { get; set; }

    /// <summary>
    /// Unique identifier for the stream session
    /// </summary>
    public required string StreamId { get; set; }

    /// <summary>
    /// ISO 8601 timestamp when the stream started
    /// </summary>
    public DateTimeOffset StartedAt { get; set; }
}