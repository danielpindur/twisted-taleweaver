using TwistedTaleweaver.Api.Messages.Queries;
using TwistedTaleweaver.Gateway.Messages.Contracts;
using TwistedTaleweaver.Gateway.Messages.Mappers;

namespace TwistedTaleweaver.Gateway.Messages.DataLoaders;

public class ExpeditionNarrativeMessagesDataLoader(
    IBatchScheduler batchScheduler,
    DataLoaderOptions options,
    INarrativeMessagesQueryHandler narrativeMessagesQueryHandler)
    : BatchDataLoader<Guid, List<ChatMessage>>(batchScheduler, options)
{
    protected override async Task<IReadOnlyDictionary<Guid, List<ChatMessage>>> LoadBatchAsync(
        IReadOnlyList<Guid> keys, CancellationToken cancellationToken)
    {
        var narrativeMessagesByExpeditionId = await narrativeMessagesQueryHandler.ExecuteAsync(
            new GetNarrativeMessagesQuery() { ExpeditionIds = keys.ToList() });

        return narrativeMessagesByExpeditionId.ToContracts();
    }
}