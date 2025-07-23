using TwistedTaleweaver.DataAccess.Expeditions.Entities.Enums;

namespace TwistedTaleweaver.DataAccess.Expeditions.Entities;

public class EncounterOutcome
{
    public Guid EncounterId { get; init; }
    
    public EncounterResult Result { get; init; }
    
    public required Monster Enemy { get; init; }
    
    public required IReadOnlyList<Guid> CharacterDeaths { get; init; }
    
    public record Monster
    {
        public Guid MonsterId { get; init; }
        
        public required string Name { get; init; }
    }
}