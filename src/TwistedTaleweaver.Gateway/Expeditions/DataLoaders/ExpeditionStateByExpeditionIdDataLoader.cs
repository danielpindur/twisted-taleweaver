using TwistedTaleweaver.Api.Expeditions.Queries;
using TwistedTaleweaver.Gateway.Expeditions.Contracts;
using TwistedTaleweaver.Gateway.Expeditions.Mappers;

namespace TwistedTaleweaver.Gateway.Expeditions.DataLoaders;

public class ExpeditionStateByExpeditionIdDataLoader(
    IBatchScheduler batchScheduler,
    DataLoaderOptions options,
    IExpeditionStatesQueryHandler expeditionStatesQueryHandler)
    : BatchDataLoader<Guid, ExpeditionState>(batchScheduler, options)
{
    protected override async Task<IReadOnlyDictionary<Guid, ExpeditionState>> LoadBatchAsync(
        IReadOnlyList<Guid> keys, CancellationToken cancellationToken)
    {
        var states = await expeditionStatesQueryHandler.ExecuteAsync(
            new ExpeditionStatesQuery() { ExpeditionIds = keys.ToList() });

        return states.ToDictionary(x => x.ExpeditionId, y => y.ToContract());
    }
}