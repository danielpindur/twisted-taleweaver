namespace TwistedTaleweaver.Expeditions.Entities.Inputs;

public class ExpeditionInput
{
    public Guid ExpeditionId { get; init; }
    
    public required List<CharacterInput> Characters { get; init; }
    
    public required List<EncounterInput> Encounters { get; init; }
}