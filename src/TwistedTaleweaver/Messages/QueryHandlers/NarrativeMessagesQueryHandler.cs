using TwistedTaleweaver.Api.Messages.Entities;
using TwistedTaleweaver.Api.Messages.Entities.Enums;
using TwistedTaleweaver.Api.Messages.Queries;
using TwistedTaleweaver.DataAccess.Messages.Readers;
using TwistedTaleweaver.Messages.Mappers;

namespace TwistedTaleweaver.Messages.QueryHandlers;

internal class NarrativeMessagesQueryHandler(IMessagesReader messagesReader) : INarrativeMessagesQueryHandler
{
    public async Task<Dictionary<Guid, List<ChatMessage>>> ExecuteAsync(GetNarrativeMessagesQuery query)
    {
        var narrativeMessages = await messagesReader.GetAsync(query.ExpeditionIds, MessageType.Narrative);

        var narrativeMessagesByExpeditionId = narrativeMessages
            .GroupBy(x => x.ExpeditionId)
            .ToDictionary(x => x.Key, y => y.ToEntities());

        foreach (var expeditionId in query.ExpeditionIds)
        {
            narrativeMessagesByExpeditionId.TryAdd(expeditionId, new List<ChatMessage>());
        }
        
        return narrativeMessagesByExpeditionId;
    }
}