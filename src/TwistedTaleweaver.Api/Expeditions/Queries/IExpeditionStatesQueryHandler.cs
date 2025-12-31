using TwistedTaleweaver.Api.Expeditions.Entities;
using TwistedTaleweaver.Core.Queries;

namespace TwistedTaleweaver.Api.Expeditions.Queries;

public class ExpeditionStatesQuery
{
    public required List<Guid> ExpeditionIds { get; set; }
}

public class ExpeditionStateQuery
{
    public Guid ExpeditionId { get; set; }
}

public interface IExpeditionStatesQueryHandler : 
    IQueryHandler<ExpeditionStatesQuery, IEnumerable<ExpeditionState>>,
    IQueryHandler<ExpeditionStateQuery, ExpeditionState>
{
}