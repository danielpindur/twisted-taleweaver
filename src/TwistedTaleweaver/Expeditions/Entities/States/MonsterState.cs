using System.Diagnostics.CodeAnalysis;
using TwistedTaleweaver.Expeditions.Entities.Inputs;

namespace TwistedTaleweaver.Expeditions.Entities.States;

public class MonsterState
{
    [SetsRequiredMembers]
    public MonsterState(MonsterInput input)
    {
        MonsterId = input.MonsterId;
        Name = input.Name;
        Health = new HealthState(input.Health);
    }
    
    public Guid MonsterId { get; init; }
    
    public required string Name { get; init; }
    
    public required HealthState Health { get; init; }
}