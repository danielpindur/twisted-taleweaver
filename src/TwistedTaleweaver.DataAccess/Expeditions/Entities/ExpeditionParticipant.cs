namespace TwistedTaleweaver.DataAccess.Expeditions.Entities;

public class ExpeditionParticipant
{
    public Guid ExpeditionId { get; set; }
    
    public Guid CharacterId { get; set; }
    
    public Guid UserId { get; set; }
    
    public required string ExternalUserId { get; set; }
}