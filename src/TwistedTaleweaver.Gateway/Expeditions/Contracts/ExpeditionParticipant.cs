namespace TwistedTaleweaver.Gateway.Expeditions.Contracts;

/// <summary>
/// Represents a participant in an expedition
/// </summary>
public class ExpeditionParticipant
{
    /// <summary>
    /// The unique identifier of the user participating in the expedition
    /// </summary>
    public Guid UserId { get; set; }
}