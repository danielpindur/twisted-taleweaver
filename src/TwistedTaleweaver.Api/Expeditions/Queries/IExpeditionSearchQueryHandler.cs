using TwistedTaleweaver.Api.Expeditions.Entities;
using TwistedTaleweaver.Api.Expeditions.Entities.Enums;
using TwistedTaleweaver.Core.Pagination;
using TwistedTaleweaver.Core.Pagination.Queries;
using TwistedTaleweaver.Core.Queries;

namespace TwistedTaleweaver.Api.Expeditions.Queries;

public class ExpeditionSearchQuery : PaginationQuery
{
    public Guid UserId { get; set; }
    
    public required List<ExpeditionStatus> Statuses { get; set; }
    
    public ExpeditionDifficulty? Difficulty { get; set; }
    
    public ExpeditionOutcome? Outcome { get; set; }
    
    public string? SearchText { get; set; }
}

public interface IExpeditionSearchQueryHandler : IQueryHandler<ExpeditionSearchQuery, PaginatedResult<Expedition>>
{
}