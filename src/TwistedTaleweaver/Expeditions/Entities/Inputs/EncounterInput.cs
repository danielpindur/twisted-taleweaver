namespace TwistedTaleweaver.Expeditions.Entities.Inputs;

public class EncounterInput
{
    public Guid EncounterId { get; init; }
    
    public required MonsterInput Monster { get; init; }
}