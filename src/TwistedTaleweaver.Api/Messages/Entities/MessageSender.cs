using TwistedTaleweaver.Api.Messages.Entities.Enums;

namespace TwistedTaleweaver.Api.Messages.Entities;

/// <summary>
/// A sender of a action message
/// </summary>
public class MessageSender
{
    /// <summary>
    /// Unique identifier of the creature
    /// </summary>
    public Guid CreatureId { get; set; }

    /// <summary>
    /// <see cref="SenderType"/>
    /// </summary>
    public SenderType SenderType { get; set; }
}