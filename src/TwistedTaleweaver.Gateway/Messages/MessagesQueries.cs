using HotChocolate.Authorization;
using TwistedTaleweaver.Api.Messages.Queries;
using TwistedTaleweaver.Gateway.Common.Pagination.Contracts;
using TwistedTaleweaver.Gateway.Messages.Contracts;
using TwistedTaleweaver.Gateway.Messages.Mappers;

namespace TwistedTaleweaver.Gateway.Messages;

[QueryType]
public static class MessagesQueries
{
    [GraphQLDescription("Retrieves a paginated list of messages for a given expedition.")]
    [Authorize]
    public static async Task<CursorPage<ChatMessage, long>> GetMessagesAsync(
        MessageSearchInput input,
        [Service] IExpeditionMessagesSearchQueryHandler expeditionMessagesSearchQueryHandler)
    {
        var paginatedMessages = await expeditionMessagesSearchQueryHandler.ExecuteAsync(
            new ExpeditionMessagesSearchQuery()
            {
                ExpeditionId = input.ExpeditionId,
                LastExpeditionMessageId = input.After,
                PageSize = input.PageSize
            });

        return paginatedMessages.ToContract();
    }
}