using System.ComponentModel.DataAnnotations;
using TwistedTaleweaver.Domain.Messages.Enums;

namespace TwistedTaleweaver.Domain.Messages;

/// <summary>
/// Represents a single chat message within the expedition
/// </summary>
public class ChatMessage
{
    /// <summary>
    /// Unique identifier of the message
    /// </summary>
    [Required]
    public long MessageId { get; set; }

    /// <summary>
    /// <see cref="MessageSender"/>
    /// </summary>
    public required MessageSender? Sender { get; set; }

    /// <summary>
    /// <see cref="MessageType"/>
    /// </summary>
    [Required]
    public MessageType Type { get; set; }

    /// <summary>
    /// The string content of the message
    /// </summary>
    [Required]
    public required string Content { get; set; }

    /// <summary>
    /// Timestamp of when the message was created within the system
    /// </summary>
    [Required]
    public DateTimeOffset Timestamp { get; set; }
}