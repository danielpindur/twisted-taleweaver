using TwistedTaleweaver.Api.Creatures.Queries;
using TwistedTaleweaver.Gateway.Creatures.Contracts;
using TwistedTaleweaver.Gateway.Creatures.Mappers;

namespace TwistedTaleweaver.Gateway.Creatures.DataLoaders;

public class CreatureByIdDataLoader(
    IBatchScheduler batchScheduler,
    DataLoaderOptions options,
    ICreaturesQueryHandler creaturesQueryHandler)
    : BatchDataLoader<Guid, Creature>(batchScheduler, options)
{
    protected override async Task<IReadOnlyDictionary<Guid, Creature>> LoadBatchAsync(
        IReadOnlyList<Guid> keys, CancellationToken cancellationToken)
    {
        var creatures = await creaturesQueryHandler.ExecuteAsync(
            new CreaturesByIdQuery() { CreatureIds = keys.ToList() });

        return creatures.ToDictionary(
            x => x.CreatureId,
            y => y.ToContract());
    }
}