using TwistedTaleweaver.Api.Creatures.Queries;
using TwistedTaleweaver.Api.Expeditions.Queries;
using TwistedTaleweaver.DataAccess.Expeditions.Readers;
using TwistedTaleweaver.Domain.Creatures;
using TwistedTaleweaver.Domain.Expeditions;
using TwistedTaleweaver.Expeditions.Mappers;
using TwistedTaleweaver.Integration.Clerk.Users;

namespace TwistedTaleweaver.Expeditions.QueryHandlers;

internal class ExpeditionParticipantsQueryHandler(
    IExpeditionParticipantsReader expeditionParticipantsReader,
    ICharacterByIdQueryHandler characterByIdQueryHandler,
    IUsersAdapter usersAdapter) : IExpeditionParticipantsQueryHandler
{
    public async Task<List<ExpeditionParticipant>> ExecuteAsync(ExpeditionParticipantsQuery query)
    {
        var participants = (await expeditionParticipantsReader.GetByExpeditionAsync(query.ExpeditionId)).ToList();
        
        var externalUserIds = participants.Select(x => x.ExternalUserId);
        var userByExternalId = await usersAdapter.GetUsersAsync(externalUserIds);
        
        var charactersById = new Dictionary<Guid, Character>();

        foreach (var participant in participants)
        {
            if (participant.CharacterId is null)
            {
                continue;
            }

            var character = await characterByIdQueryHandler.ExecuteAsync(
                new CharacterByIdQuery() { CharacterId = participant.CharacterId.Value });

            if (character is null)
            {
                continue;
            }
            
            charactersById.Add(participant.CharacterId.Value, character);
        }
        
        return participants.ToExpeditionParticipants(userByExternalId, charactersById);
    }
}