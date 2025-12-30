using TwistedTaleweaver.Gateway.Creatures.DataLoaders;

namespace TwistedTaleweaver.Gateway.Creatures.Contracts;

[ObjectType<Character>]
public static partial class CharacterNode
{
    [GraphQLDescription("The traits of the character.")]
    public static async Task<List<CreatureTrait>> GetTraitsAsync(
        [Parent] Character character,
        CreatureTraitsByCreatureIdDataLoader dataLoader,
        CancellationToken cancellationToken)
    {
        var creatureTraits = await dataLoader.LoadAsync(character.Id, cancellationToken);
        return creatureTraits ??
               throw new InvalidOperationException($"Creature traits not found for character with ID {character.Id}");
    }
}