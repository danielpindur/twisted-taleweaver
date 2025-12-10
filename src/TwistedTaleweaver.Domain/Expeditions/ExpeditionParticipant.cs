using TwistedTaleweaver.Domain.Creatures;
using TwistedTaleweaver.Domain.Users;

namespace TwistedTaleweaver.Domain.Expeditions;

/// <summary>
/// Represents a participant in an expedition
/// </summary>
public class ExpeditionParticipant : User
{
    /// <summary>
    /// The character associated with the participant, if any
    /// </summary>
    public Character? Character { get; set; }
}