namespace TwistedTaleweaver.Expeditions.Entities.Outcomes;

public class EncounterOutcome
{
    public Guid EncounterId { get; init; }
    
    public List<string> Narration { get; init; } = [];
}