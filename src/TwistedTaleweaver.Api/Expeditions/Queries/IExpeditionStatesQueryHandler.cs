using TwistedTaleweaver.Core.Queries;
using TwistedTaleweaver.Domain.Expeditions;

namespace TwistedTaleweaver.Api.Expeditions.Queries;

public class ExpeditionStatesQuery
{
    public required List<Guid> ExpeditionIds { get; set; }
}

public interface IExpeditionStatesQueryHandler : IQueryHandler<ExpeditionStatesQuery, IEnumerable<ExpeditionState>>
{
}