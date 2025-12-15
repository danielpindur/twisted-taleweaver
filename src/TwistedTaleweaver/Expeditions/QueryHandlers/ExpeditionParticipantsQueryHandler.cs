using TwistedTaleweaver.Api.Expeditions.Queries;
using TwistedTaleweaver.DataAccess.Expeditions.Readers;
using TwistedTaleweaver.Domain.Expeditions;
using TwistedTaleweaver.Expeditions.Mappers;
using TwistedTaleweaver.Integration.Clerk.Users;

namespace TwistedTaleweaver.Expeditions.QueryHandlers;

internal class ExpeditionParticipantsQueryHandler(
    IExpeditionParticipantsReader expeditionParticipantsReader,
    IUsersAdapter usersAdapter) : IExpeditionParticipantsQueryHandler
{
    public async Task<List<ExpeditionParticipant>> ExecuteAsync(ExpeditionParticipantsQuery query)
    {
        var participants = (await expeditionParticipantsReader.GetByExpeditionAsync(query.ExpeditionId)).ToList();
        
        var externalUserIds = participants.Select(x => x.ExternalUserId);
        var userByExternalId = await usersAdapter.GetUsersAsync(externalUserIds);
        
        return participants.ToExpeditionParticipants(userByExternalId);
    }
    
    public async Task<Dictionary<Guid, List<ExpeditionParticipant>>> ExecuteAsync(ExpeditionsParticipantsQuery query)
    {
        var participants = (await expeditionParticipantsReader.GetByExpeditionsAsync(query.ExpeditionIds)).ToList();

        var externalUserIds = participants.Select(x => x.ExternalUserId).Distinct();
        var userByExternalId = await usersAdapter.GetUsersAsync(externalUserIds);
        
        return participants
            .GroupBy(x => x.ExpeditionId)
            .ToDictionary(
                x => x.Key,
                x => x.ToExpeditionParticipants(userByExternalId));
    }
}