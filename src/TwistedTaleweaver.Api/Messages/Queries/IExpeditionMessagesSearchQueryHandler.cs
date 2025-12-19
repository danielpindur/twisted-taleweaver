using TwistedTaleweaver.Core.Queries;
using TwistedTaleweaver.Domain.Messages;

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