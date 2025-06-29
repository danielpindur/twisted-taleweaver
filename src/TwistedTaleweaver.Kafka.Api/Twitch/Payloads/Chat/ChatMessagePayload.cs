using TwistedTaleweaver.Core.Kafka;
using TwistedTaleweaver.Core.Kafka.Attributes;

namespace TwistedTaleweaver.Kafka.Api.Twitch.Payloads.Chat;

/// <summary>
/// Represents a chat message payload for Twitch EventSub notifications.
/// </summary>
public class ChatMessagePayload : EventPayload
{
    /// <summary>
    /// Unique identifier for the chat message
    /// </summary>
    public required string MessageId { get; set; }

    /// <summary>
    /// Unique identifier for the broadcaster whose chat this is
    /// </summary>
    [PartitionKey]
    public required string BroadcasterUserId { get; set; }

    /// <summary>
    /// Unique identifier for the user who sent the message
    /// </summary>
    public required string ChatterUserId { get; set; }

    /// <summary>
    /// Display name of the user who sent the message
    /// </summary>
    public required string ChatterUserName { get; set; }

    /// <summary>
    /// The chat message content
    /// </summary>
    public required string Message { get; set; }
}