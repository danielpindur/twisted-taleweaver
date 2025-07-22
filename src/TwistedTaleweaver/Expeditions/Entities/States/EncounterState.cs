using System.Diagnostics.CodeAnalysis;
using TwistedTaleweaver.Expeditions.Entities.Inputs;

namespace TwistedTaleweaver.Expeditions.Entities.States;

public class EncounterState
{
    [SetsRequiredMembers]
    public EncounterState(List<CharacterState> characters, EncounterInput encounter)
    {
        AliveCharacters = characters;
        Monster = new MonsterState(encounter.Monster);
        EncounterId = encounter.EncounterId;
    }
    
    public Guid EncounterId { get; set; }
    
    public required List<CharacterState> AliveCharacters { get; init; }
    
    public required MonsterState Monster { get; init; }
}