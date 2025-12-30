using TwistedTaleweaver.Api.Creatures.Entities;
using TwistedTaleweaver.Api.Creatures.Queries;
using TwistedTaleweaver.Api.Expeditions.Entities;
using TwistedTaleweaver.Api.Expeditions.Queries;
using TwistedTaleweaver.DataAccess.Expeditions.Readers;

namespace TwistedTaleweaver.Expeditions.QueryHandlers;

internal class ExpeditionParticipantsQueryHandler(
    IExpeditionParticipantsReader expeditionParticipantsReader,
    ICharacterByIdQueryHandler characterByIdQueryHandler) : IExpeditionParticipantsQueryHandler
{
    public async Task<Dictionary<Guid, List<ExpeditionParticipant>>> ExecuteAsync(ExpeditionParticipantsQuery query)
    {
        var participants = await expeditionParticipantsReader.GetByExpeditionsAsync(query.ExpeditionIds);
        var result = new Dictionary<Guid, List<ExpeditionParticipant>>();
        
        // TODO: Do this in bulk
        foreach (var participant in participants)
        {
            if (!result.ContainsKey(participant.ExpeditionId))
            {
                result.Add(participant.ExpeditionId, new List<ExpeditionParticipant>());
            }
            
            var character = await GetCharacterAsync(participant.CharacterId);
            
            result[participant.ExpeditionId].Add(new ExpeditionParticipant()
            {
                UserId = participant.UserId,
                Character = character,
            });
        }

        return result;
    }

    private async Task<Character?> GetCharacterAsync(Guid? characterId)
    {
        if (characterId is null)
        {
            return null;
        }
        
        return await characterByIdQueryHandler.ExecuteAsync(
            new CharacterByIdQuery() { CharacterId = characterId.Value });
    }
}