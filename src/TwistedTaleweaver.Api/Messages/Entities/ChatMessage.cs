using TwistedTaleweaver.Api.Messages.Entities.Enums;

namespace TwistedTaleweaver.Api.Messages.Entities;

/// <summary>
/// Represents a single chat message within the expedition
/// </summary>
public class ChatMessage
{
    /// <summary>
    /// Unique identifier of the message
    /// </summary>
    public long MessageId { get; set; }

    /// <summary>
    /// <see cref="MessageSender"/>
    /// </summary>
    public required MessageSender? Sender { get; set; }

    /// <summary>
    /// <see cref="MessageType"/>
    /// </summary>
    public MessageType Type { get; set; }

    /// <summary>
    /// The string content of the message
    /// </summary>
    public required string Content { get; set; }

    /// <summary>
    /// Timestamp of when the message was created within the system
    /// </summary>
    public DateTimeOffset Timestamp { get; set; }
}