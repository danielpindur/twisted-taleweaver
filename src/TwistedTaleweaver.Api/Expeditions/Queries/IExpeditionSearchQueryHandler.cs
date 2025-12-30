using TwistedTaleweaver.Api.Expeditions.Entities;
using TwistedTaleweaver.Core.Pagination;
using TwistedTaleweaver.Core.Pagination.Queries;
using TwistedTaleweaver.Core.Queries;

namespace TwistedTaleweaver.Api.Expeditions.Queries;

public class ExpeditionSearchQuery : PaginationQuery
{
    public Guid UserId { get; set; }
}

public interface IExpeditionSearchQueryHandler : IQueryHandler<ExpeditionSearchQuery, PaginatedResult<Expedition>>
{
}