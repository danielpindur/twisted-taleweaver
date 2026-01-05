using TwistedTaleweaver.Api.Messages.Queries;
using TwistedTaleweaver.DataAccess.Messages.Readers;
using TwistedTaleweaver.Messages.Mappers;

namespace TwistedTaleweaver.Messages.QueryHandlers;

internal class ExpeditionMessagesSearchQueryHandler(IMessagesReader messagesReader) : IExpeditionMessagesSearchQueryHandler
{
    public async Task<PaginatedMessagesSearchResult> ExecuteAsync(ExpeditionMessagesSearchQuery query)
    {
        var messages = (await messagesReader
            .SearchAsync(query.ExpeditionId, query.LastExpeditionMessageId, query.PageSize + 1)).ToList();
        
        var hasNextPage = messages.Count > query.PageSize;
        var items = messages.Take(query.PageSize).ToList();
        items.Reverse();
        
        return items.ToPaginatedResult(hasNextPage);
    }
}