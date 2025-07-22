using TwistedTaleweaver.DataAccess.Expeditions.Entities.Enums;

namespace TwistedTaleweaver.DataAccess.Expeditions.Entities;

public class ExpeditionOutcome
{
    public Guid ExpeditionId { get; init; }
    
    public ExpeditionResult Result { get; set; }
    
    public List<EncounterOutcome> Encounters { get; init; } = [];
    
    public List<string> Narrations { get; init; } = [];
}