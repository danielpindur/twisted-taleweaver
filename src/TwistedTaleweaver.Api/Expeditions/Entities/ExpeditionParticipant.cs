namespace TwistedTaleweaver.Api.Expeditions.Entities;

/// <summary>
/// Represents a participant in an expedition
/// </summary>
public class ExpeditionParticipant
{
    /// <summary>
    /// Unique identifier of the user participating in the expedition
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// Unique identifier of the character associated with the participant, if any
    /// </summary>
    public Guid? CharacterId { get; set; }
}