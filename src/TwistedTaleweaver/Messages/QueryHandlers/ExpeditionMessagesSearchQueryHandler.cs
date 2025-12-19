using TwistedTaleweaver.Api.Messages.Queries;
using TwistedTaleweaver.DataAccess.Messages.Readers;
using TwistedTaleweaver.Domain.Messages;
using TwistedTaleweaver.Messages.Mappers;

namespace TwistedTaleweaver.Messages.QueryHandlers;

internal class ExpeditionMessagesSearchQueryHandler(IMessagesReader messagesReader) : IExpeditionMessagesSearchQueryHandler
{
    public async Task<List<ChatMessage>> ExecuteAsync(ExpeditionMessagesSearchQuery query)
    {
        var messages = await messagesReader
            .SearchAsync(query.ExpeditionId, query.LastExpeditionMessageId, query.PageSize);

        return messages.ToChatMessages();
    }
}