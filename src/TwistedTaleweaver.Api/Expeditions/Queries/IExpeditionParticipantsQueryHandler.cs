using TwistedTaleweaver.Core.Queries;
using TwistedTaleweaver.Domain.Expeditions;

namespace TwistedTaleweaver.Api.Expeditions.Queries;

public class ExpeditionParticipantsQuery
{
    public required List<Guid> ExpeditionIds { get; set; }
}

public interface IExpeditionParticipantsQueryHandler :
    IQueryHandler<ExpeditionParticipantsQuery, Dictionary<Guid, List<ExpeditionParticipant>>>
{
}