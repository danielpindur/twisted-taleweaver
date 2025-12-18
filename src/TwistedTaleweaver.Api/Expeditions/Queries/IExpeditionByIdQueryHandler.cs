using TwistedTaleweaver.Core.Queries;
using TwistedTaleweaver.Domain.Expeditions;

namespace TwistedTaleweaver.Api.Expeditions.Queries;

public class ExpeditionByIdQuery
{
    public Guid ExpeditionId { get; set; }
}

public interface IExpeditionByIdQueryHandler : IQueryHandler<ExpeditionByIdQuery, Expedition?>
{
}