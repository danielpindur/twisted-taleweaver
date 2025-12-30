using TwistedTaleweaver.Api.Creatures.Queries;
using TwistedTaleweaver.Gateway.Creatures.Contracts;
using TwistedTaleweaver.Gateway.Creatures.Mappers;

namespace TwistedTaleweaver.Gateway.Creatures.DataLoaders;

public class CreatureTraisByCreatureIdDataLoader(
    IBatchScheduler batchScheduler,
    DataLoaderOptions options,
    ICreatureTraitsQueryHandler creatureTraitsQueryHandler)
    : BatchDataLoader<Guid, List<CreatureTrait>>(batchScheduler, options)
{
    protected override async Task<IReadOnlyDictionary<Guid, List<CreatureTrait>>> LoadBatchAsync(
        IReadOnlyList<Guid> keys, CancellationToken cancellationToken)
    {
        var traitsByCreatureId = await creatureTraitsQueryHandler.ExecuteAsync(
            new CreatureTraitsQuery() { CreatureIds = keys.ToList() });

        return traitsByCreatureId.ToContracts();
    }
}