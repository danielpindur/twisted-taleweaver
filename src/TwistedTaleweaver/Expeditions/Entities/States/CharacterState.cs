using System.Diagnostics.CodeAnalysis;
using TwistedTaleweaver.Expeditions.Entities.Inputs;

namespace TwistedTaleweaver.Expeditions.Entities.States;

public class CharacterState
{
    [SetsRequiredMembers]
    public CharacterState(CharacterInput input)
    {
        CharacterId = input.CharacterId;
        ExternalUserId = input.ExternalUserId;
        Health = new HealthState(input.Health);
    }
    
    public Guid CharacterId { get; init; }
    
    public required string ExternalUserId { get; init; }
    
    public required HealthState Health { get; init; }
}