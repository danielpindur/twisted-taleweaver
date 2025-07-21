using System.Diagnostics.CodeAnalysis;
using TwistedTaleweaver.Expeditions.Entities.Inputs;

namespace TwistedTaleweaver.Expeditions.Entities.States;

public class ExpeditionState
{
    [SetsRequiredMembers]
    public ExpeditionState(ExpeditionInput input)
    {
        ExpeditionId = input.ExpeditionId;
        Characters = input.Characters.Select(x => new CharacterState(x)).ToList();
        RemainingEncounters = new Queue<EncounterInput>(input.Encounters);
    }
    
    public Guid ExpeditionId { get; init; }
    
    public required List<CharacterState> Characters { get; init; }
    
    public required Queue<EncounterInput> RemainingEncounters { get; init; }

    public EncounterState? NextEncounter()
    {
        if (RemainingEncounters.Count <= 0)
        {
            return null;
        }
        
        var encounterInput = RemainingEncounters.Dequeue();

        return new EncounterState(Characters, encounterInput);
    }
    
    public void RemoveDeadCharacters() => Characters.RemoveAll(x => x.Health.Current <= 0);
}