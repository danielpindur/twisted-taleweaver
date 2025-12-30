using TwistedTaleweaver.Api.Messages.Entities;
using TwistedTaleweaver.Core.Queries;

namespace TwistedTaleweaver.Api.Messages.Queries;

public class ExpeditionMessagesSearchQuery
{
    public Guid ExpeditionId { get; set; }

    public int? LastExpeditionMessageId { get; set; }

    public int PageSize { get; set; } = 20;
}

public interface IExpeditionMessagesSearchQueryHandler : IQueryHandler<ExpeditionMessagesSearchQuery, List<ChatMessage>>
{
}