namespace TwistedTaleweaver.Gateway.Creatures.Contracts;

[GraphQLDescription("Represents a creature.")]
public class Creature
{
    [GraphQLDescription("Unique identifier of the creature.")]
    public Guid Id { get; set; }
    
    [GraphQLDescription("The name of the creature.")]
    public required string Name { get; set; }
}