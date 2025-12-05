using TwistedTaleweaver.Expeditions.Entities.Enums;

namespace TwistedTaleweaver.Expeditions.Entities;

public class Expedition
{
    public Guid ExpeditionId { get; set; }
    
    public required string Name { get; set; }
    
    public required string Description { get; set; }
    
    public ExpeditionStatus Status { get; set; }
    
    public ExpeditionDifficulty  Difficulty { get; set; }
    
    public byte MaxParticipants { get; set; }

    public ExpeditionOutcome? Outcome { get; set; }
    
    public required List<ExpeditionParticipant> Participants { get; set; }
    
    public DateTimeOffset CreatedAt { get; set; }
    
    public DateTimeOffset UpdatedAt { get; set; }
    
    public DateTimeOffset? StartedAt { get; set; }
    
    public DateTimeOffset? CompletedAt { get; set; }
}