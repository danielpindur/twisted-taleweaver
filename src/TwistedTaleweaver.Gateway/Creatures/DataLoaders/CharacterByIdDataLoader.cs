using TwistedTaleweaver.Api.Creatures.Queries;
using TwistedTaleweaver.Gateway.Creatures.Mappers;
using Character = TwistedTaleweaver.Gateway.Creatures.Contracts.Character;

namespace TwistedTaleweaver.Gateway.Creatures.DataLoaders;

public class CharacterByIdDataLoader(
    IBatchScheduler batchScheduler,
    DataLoaderOptions options,
    ICharactersQueryHandler charactersQueryHandler)
    : BatchDataLoader<Guid, Character>(batchScheduler, options)
{
    protected override async Task<IReadOnlyDictionary<Guid, Character>> LoadBatchAsync(
        IReadOnlyList<Guid> keys, CancellationToken cancellationToken)
    {
        var characters = await charactersQueryHandler.ExecuteAsync(
            new CharactersByIdQuery() { CharacterIds = keys.ToList() });

        return characters.ToDictionary(x => x.CreatureId, y => y.ToContract());
    }
}