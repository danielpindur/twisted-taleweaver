using TwistedTaleweaver.Api.Creatures.Entities;
using TwistedTaleweaver.Api.Creatures.Queries;
using TwistedTaleweaver.DataAccess.Creatures.Readers;

namespace TwistedTaleweaver.Creatures.QueryHandlers;

internal class CombatCreatureStatesQueryHandler(ICreaturesReader creaturesReader) : ICombatCreatureStatesQueryHandler
{
    public async Task<Dictionary<Guid, List<CombatCreatureState>>> ExecuteAsync(CharactersStatesQuery query)
    {
        var charactersStates = await creaturesReader.GetCharacterStatesByExpeditionsAsync(query.ExpeditionIds);

        var characterStatesByExpeditionId = charactersStates
            .GroupBy(x => x.ExpeditionId)
            .ToDictionary(
                x => x.Key,
                y => y.Select(z => new CombatCreatureState()
                {
                    CreatureId = z.CreatureId,
                    Status = z.Status,
                    CurrentHealth = 10
                }).ToList());

        foreach (var expeditionId in query.ExpeditionIds)
        {
            if (!characterStatesByExpeditionId.ContainsKey(expeditionId))
            {
                characterStatesByExpeditionId[expeditionId] = new List<CombatCreatureState>();
            }
        }
        
        return characterStatesByExpeditionId;
    }
}