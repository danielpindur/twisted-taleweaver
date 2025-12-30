using TwistedTaleweaver.Api.Expeditions.Entities;
using TwistedTaleweaver.Core.Queries;

namespace TwistedTaleweaver.Api.Expeditions.Queries;

public class ExpeditionByIdQuery
{
    public Guid ExpeditionId { get; set; }
}

public interface IExpeditionByIdQueryHandler : IQueryHandler<ExpeditionByIdQuery, Expedition?>
{
}