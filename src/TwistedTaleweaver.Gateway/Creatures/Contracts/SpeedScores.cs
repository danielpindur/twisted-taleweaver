namespace TwistedTaleweaver.Gateway.Creatures.Contracts;

[GraphQLDescription("Represents how far a creature can move on its turn during combat.")]
public class SpeedScores
{
    [GraphQLDescription("Default walking speed in feet.")]
    public int Walking { get; init; }

    [GraphQLDescription("Represents the creature's speed while flying, if applicable.")]
    public int? Flying { get; init; }

    [GraphQLDescription("Represents the creature's speed while swimming, if applicable.")]
    public int? Swimming { get; init; }

    [GraphQLDescription("Represents the creature's speed while climbing, if applicable.")]
    public int? Climbing { get; init; }

    [GraphQLDescription("Represents the creature's speed while burrowing, if applicable.")]
    public int? Burrowing { get; init; }

    [GraphQLDescription("Represents the creature's speed while teleporting, if applicable.")]
    public int? Teleportation { get; init; }
}