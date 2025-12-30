namespace TwistedTaleweaver.Gateway.Creatures.Contracts;

[GraphQLDescription("Passive ability or feature that is always active or trigger under certain conditions.")]
public class CreatureTrait
{
    [GraphQLDescription("Name of the trait.")]
    public required string Name { get; init; }

    [GraphQLDescription("Description of the trait's effect or purpose.")]
    public required string Effect { get; init; }

    [GraphQLDescription("Optional. Condition under which the trait is active or applies.")]
    public string? ConditionForPerform { get; init; }
}