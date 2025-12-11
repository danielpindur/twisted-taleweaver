using TwistedTaleweaver.Core.Queries;
using TwistedTaleweaver.Domain.Expeditions;

namespace TwistedTaleweaver.Api.Expeditions.Queries;

public class ExpeditionParticipantsQuery
{
    public Guid ExpeditionId { get; set; }
}

public class ExpeditionsParticipantsQuery
{
    public required List<Guid> ExpeditionIds { get; set; }
}

public interface IExpeditionParticipantsQueryHandler : 
    IQueryHandler<ExpeditionsParticipantsQuery, Dictionary<Guid, List<ExpeditionParticipant>>>, 
    IQueryHandler<ExpeditionParticipantsQuery, List<ExpeditionParticipant>>
{
}