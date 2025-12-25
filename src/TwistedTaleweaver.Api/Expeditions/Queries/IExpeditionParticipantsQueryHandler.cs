using TwistedTaleweaver.Core.Queries;
using TwistedTaleweaver.Domain.Expeditions;

namespace TwistedTaleweaver.Api.Expeditions.Queries;

public class ExpeditionParticipantsQuery
{
    public Guid ExpeditionId { get; set; }
}

public interface IExpeditionParticipantsQueryHandler :
    IQueryHandler<ExpeditionParticipantsQuery, List<ExpeditionParticipant>>
{
}