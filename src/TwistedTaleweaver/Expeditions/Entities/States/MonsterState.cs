using System.Diagnostics.CodeAnalysis;
using TwistedTaleweaver.Expeditions.Entities.Inputs;

namespace TwistedTaleweaver.Expeditions.Entities.States;

public class MonsterState : Damageable
{
    [SetsRequiredMembers]
    public MonsterState(MonsterInput input) : base(input.Health)
    {
        MonsterId = input.MonsterId;
        Name = input.Name;
    }
    
    public Guid MonsterId { get; init; }
    
    public required string Name { get; init; }
}