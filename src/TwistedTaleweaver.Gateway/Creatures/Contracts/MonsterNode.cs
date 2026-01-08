using TwistedTaleweaver.Gateway.Creatures.DataLoaders;

namespace TwistedTaleweaver.Gateway.Creatures.Contracts;

[ObjectType<Monster>]
public static partial class MonsterNode
{
    [GraphQLDescription("The traits of the monster.")]
    public static async Task<List<CreatureTrait>> GetTraitsAsync(
        [Parent] Monster monster,
        CreatureTraitsByCreatureIdDataLoader dataLoader,
        CancellationToken cancellationToken)
    {
        var creatureTraits = await dataLoader.LoadAsync(monster.Id, cancellationToken);
        return creatureTraits ??
               throw new InvalidOperationException($"Creature traits not found for monster with ID {monster.Id}");
    }
}