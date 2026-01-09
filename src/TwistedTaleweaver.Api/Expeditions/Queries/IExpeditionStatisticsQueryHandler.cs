using TwistedTaleweaver.Api.Expeditions.Entities;
using TwistedTaleweaver.Api.Expeditions.Entities.Enums;
using TwistedTaleweaver.Core.Queries;

namespace TwistedTaleweaver.Api.Expeditions.Queries;

public class ExpeditionStatisticsQuery
{
    public Guid UserId { get; set; }
    
    public required List<ExpeditionStatus> Statuses { get; set; }
    
    public ExpeditionDifficulty? Difficulty { get; set; }
    
    public ExpeditionOutcome? Outcome { get; set; }
    
    public string? SearchText { get; set; }
}

public interface IExpeditionStatisticsQueryHandler : IQueryHandler<ExpeditionStatisticsQuery, ExpeditionStatistics>
{
}