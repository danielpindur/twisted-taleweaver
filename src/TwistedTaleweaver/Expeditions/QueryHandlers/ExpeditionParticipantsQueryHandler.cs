using TwistedTaleweaver.Api.Expeditions.Entities;
using TwistedTaleweaver.Api.Expeditions.Queries;
using TwistedTaleweaver.DataAccess.Expeditions.Readers;

namespace TwistedTaleweaver.Expeditions.QueryHandlers;

internal class ExpeditionParticipantsQueryHandler(IExpeditionParticipantsReader expeditionParticipantsReader) : IExpeditionParticipantsQueryHandler
{
    public async Task<Dictionary<Guid, List<ExpeditionParticipant>>> ExecuteAsync(ExpeditionParticipantsQuery query)
    {
        var participants = await expeditionParticipantsReader.GetByExpeditionsAsync(query.ExpeditionIds);
        var result = new Dictionary<Guid, List<ExpeditionParticipant>>();
        
        foreach (var participant in participants)
        {
            if (!result.ContainsKey(participant.ExpeditionId))
            {
                result.Add(participant.ExpeditionId, new List<ExpeditionParticipant>());
            }
            
            result[participant.ExpeditionId].Add(new ExpeditionParticipant()
            {
                UserId = participant.UserId,
                CharacterId = participant.CharacterId,
            });
        }

        return result;
    }
}