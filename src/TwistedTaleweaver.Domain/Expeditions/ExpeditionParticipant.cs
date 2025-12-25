using TwistedTaleweaver.Domain.Creatures;
using TwistedTaleweaver.Domain.Users;

namespace TwistedTaleweaver.Domain.Expeditions;

/// <summary>
/// Represents a participant in an expedition
/// </summary>
public class ExpeditionParticipant
{
    /// <summary>
    /// User of the participant
    /// </summary>
    public required User User { get; set; }

    /// <summary>
    /// The character associated with the participant, if any
    /// </summary>
    public Character? Character { get; set; }
}