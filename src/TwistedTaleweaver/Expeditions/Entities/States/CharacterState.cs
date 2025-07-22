using System.Diagnostics.CodeAnalysis;
using TwistedTaleweaver.Expeditions.Entities.Inputs;

namespace TwistedTaleweaver.Expeditions.Entities.States;

public class CharacterState : Damageable
{
    [SetsRequiredMembers]
    public CharacterState(CharacterInput input) : base(input.Health)
    {
        CharacterId = input.CharacterId;
        ExternalUserId = input.ExternalUserId;
    }
    
    public Guid CharacterId { get; init; }
    
    public required string ExternalUserId { get; init; }
}