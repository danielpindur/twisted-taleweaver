using TwistedTaleweaver.Api.Messages.Entities;
using TwistedTaleweaver.Core.Queries;

namespace TwistedTaleweaver.Api.Messages.Queries;

public class ExpeditionMessagesSearchQuery
{
    public Guid ExpeditionId { get; set; }

    public long? LastExpeditionMessageId { get; set; }

    public int PageSize { get; set; } = 20;
}

public class PaginatedMessagesSearchResult
{
    public required List<ChatMessage> Messages { get; set; }
    
    public bool HasNextPage { get; set;}
}

public interface IExpeditionMessagesSearchQueryHandler : IQueryHandler<ExpeditionMessagesSearchQuery, PaginatedMessagesSearchResult>
{
}