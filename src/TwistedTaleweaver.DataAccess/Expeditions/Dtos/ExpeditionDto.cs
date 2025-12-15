using TwistedTaleweaver.DataAccess.Users.Dtos;
using TwistedTaleweaver.Domain.Expeditions.Enums;

namespace TwistedTaleweaver.DataAccess.Expeditions.Dtos;

public class ExpeditionDto
{
    public Guid ExpeditionId { get; set; }
    
    public required string Name { get; set; }
    
    public required string Description { get; set; }
    
    public ExpeditionStatus Status { get; set; }
    
    public ExpeditionDifficulty  Difficulty { get; set; }
    
    public int MinParticipants { get; set; }
    
    public int MaxParticipants { get; set; }
    
    public int CurrentParticipants { get; set; }
    
    public Guid CreatedByUserId { get; set; }
    
    public required string CreatedByExternalUserId { get; set; }

    public ExpeditionOutcome? Outcome { get; set; }
}