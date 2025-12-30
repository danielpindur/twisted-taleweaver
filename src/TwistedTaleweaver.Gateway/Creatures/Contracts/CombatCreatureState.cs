using TwistedTaleweaver.Api.Creatures.Entities.Enums;

namespace TwistedTaleweaver.Gateway.Creatures.Contracts;

[GraphQLDescription("Represents current state of a combat creature.")]
public class CombatCreatureState
{
    [GraphQLDescription("Identifier of the creature this state data is related to.")]
    public Guid CreatureId { get; set; }

    [GraphQLDescription("Current status of the creature.")]
    public CreatureStatus Status { get; set; }

    [GraphQLDescription("Remaining hit points.")]
    public int CurrentHealth { get; set; }
}