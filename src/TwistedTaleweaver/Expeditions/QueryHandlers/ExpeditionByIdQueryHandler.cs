using TwistedTaleweaver.Api.Expeditions.Queries;
using TwistedTaleweaver.DataAccess.Expeditions.Readers;
using TwistedTaleweaver.Domain.Expeditions;
using TwistedTaleweaver.Expeditions.Mappers;
using TwistedTaleweaver.Integration.Clerk.Users;

namespace TwistedTaleweaver.Expeditions.QueryHandlers;

internal class ExpeditionByIdQueryHandler(
    IExpeditionsReader expeditionsReader,
    IUsersAdapter usersAdapter,
    IExpeditionParticipantsReader expeditionParticipantsReader) : IExpeditionByIdQueryHandler
{
    public async Task<Expedition?> ExecuteAsync(ExpeditionByIdQuery query)
    {
        var dto = await expeditionsReader.GetByIdAsync(query.ExpeditionId, query.UserId);
        
        if (dto is null)
        {
            return null;
        }

        var participants = (await expeditionParticipantsReader.GetByExpeditionAsync(query.ExpeditionId)).ToList();
        
        var userByExternalId = await usersAdapter
            .GetUsersAsync(participants.Select(x => x.ExternalUserId).ToList());
        
        return dto.ToExpedition(participants, userByExternalId);
    }
}