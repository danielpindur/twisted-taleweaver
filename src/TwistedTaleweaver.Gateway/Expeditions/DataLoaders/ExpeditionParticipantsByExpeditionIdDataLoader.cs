using TwistedTaleweaver.Api.Expeditions.Queries;
using TwistedTaleweaver.Gateway.Expeditions.Contracts;
using TwistedTaleweaver.Gateway.Expeditions.Mappers;

namespace TwistedTaleweaver.Gateway.Expeditions.DataLoaders;

public class ExpeditionParticipantsByExpeditionIdDataLoader(
    IBatchScheduler batchScheduler,
    DataLoaderOptions options,
    IExpeditionParticipantsQueryHandler expeditionParticipantsQueryHandler)
    : BatchDataLoader<Guid, List<ExpeditionParticipant>>(batchScheduler, options)
{
    protected override async Task<IReadOnlyDictionary<Guid, List<ExpeditionParticipant>>> LoadBatchAsync(
        IReadOnlyList<Guid> keys, CancellationToken cancellationToken)
    {
        var participantsByExpedition = await expeditionParticipantsQueryHandler.ExecuteAsync(
            new ExpeditionParticipantsQuery() { ExpeditionIds = keys.ToList() });

        return participantsByExpedition.ToContracts();
    }
}