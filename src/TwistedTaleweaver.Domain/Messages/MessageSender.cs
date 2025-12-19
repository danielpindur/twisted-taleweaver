using System.ComponentModel.DataAnnotations;
using TwistedTaleweaver.Domain.Messages.Enums;

namespace TwistedTaleweaver.Domain.Messages;

/// <summary>
/// A sender of a action message
/// </summary>
public class MessageSender
{
    /// <summary>
    /// Unique identifier of the creature
    /// </summary>
    [Required]
    public Guid CreatureId { get; set; }
    
    /// <summary>
    /// <see cref="SenderType"/>
    /// </summary>
    [Required]
    public SenderType SenderType { get; set; }
}