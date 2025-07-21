namespace TwistedTaleweaver.Expeditions.Entities.Outcomes;

public class ExpeditionOutcome
{
    public Guid ExpeditionId { get; init; }
    
    public List<EncounterOutcome> Encounters { get; init; } = [];
    
    public string? Prologue { get; set; }
    
    public string? Epilogue { get; set; }
}