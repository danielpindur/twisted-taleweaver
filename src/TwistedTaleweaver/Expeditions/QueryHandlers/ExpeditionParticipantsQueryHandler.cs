using TwistedTaleweaver.Api.Expeditions.Queries;
using TwistedTaleweaver.DataAccess.Expeditions.Readers;
using TwistedTaleweaver.Domain.Expeditions;
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
        var clerkUsers = await usersAdapter.GetUsersAsync(externalUserIds);
        
        return participants.Select(x => new ExpeditionParticipant()
        {
            UserId = x.UserId,
            Name = clerkUsers[x.ExternalUserId].Username!
        }).ToList();
    }
    
    public async Task<Dictionary<Guid, List<ExpeditionParticipant>>> ExecuteAsync(ExpeditionsParticipantsQuery query)
    {
        var participants = (await expeditionParticipantsReader.GetByExpeditionsAsync(query.ExpeditionIds)).ToList();

        var externalUserIds = participants.Select(x => x.ExternalUserId).Distinct();
        var clerkUsers = await usersAdapter.GetUsersAsync(externalUserIds);
        
        return participants
            .GroupBy(x => x.ExpeditionId)
            .ToDictionary(
                x => x.Key,
                x => x.Select(y => new ExpeditionParticipant()
                {
                    UserId = y.UserId,
                    Name = clerkUsers[y.ExternalUserId].Username!
                }).ToList());
    }
}