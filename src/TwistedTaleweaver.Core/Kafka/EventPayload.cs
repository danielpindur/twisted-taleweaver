namespace TwistedTaleweaver.Core.Kafka;

/// <summary>
/// Base class for Kafka event payloads.
/// </summary>
public abstract class EventPayload
{
    /// <summary>
    /// Unique identifier for the event payload.
    /// </summary>
    public required string NotificationMessageId { get; set; }
    
    /// <summary>
    /// Timestamp of when the notification message was created.
    /// </summary>
    public DateTimeOffset NotificationMessageTimestamp { get; set; }
}