using TwistedTaleweaver.Core.Kafka;
using TwistedTaleweaver.Core.Kafka.Attributes;

namespace TwistedTaleweaver.Kafka.Api.Twitch.Payloads.Stream;

/// <summary>
/// Represents a stream ended payload for Twitch EventSub notifications.
/// </summary>
public class StreamEndedPayload : EventPayload
{
    /// <summary>
    /// Unique identifier for the user broadcaster
    /// </summary>
    [PartitionKey]
    public required string BroadcasterUserId { get; set; }
}