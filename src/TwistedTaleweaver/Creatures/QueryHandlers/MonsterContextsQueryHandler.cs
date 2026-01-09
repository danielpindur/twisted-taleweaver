using TwistedTaleweaver.Api.Creatures.Queries;
using TwistedTaleweaver.Core.Queries;
using TwistedTaleweaver.Creatures.Mappers;
using TwistedTaleweaver.DataAccess.Creatures.Readers;
using TwistedTaleweaver.Integration.OpenAi.Creatures.Entities;
using TwistedTaleweaver.Integration.OpenAi.Creatures.Mappers;

namespace TwistedTaleweaver.Creatures.QueryHandlers;

internal class GetMonsterContextsByExpeditionQuery
{
    public Guid ExpeditionId { get; set; }
}

internal interface IMonsterContextsQueryHandler : IQueryHandler<GetMonsterContextsByExpeditionQuery, List<MonsterContext>>
{
}

internal class MonsterContextsQueryHandler(
    ICreaturesReader creaturesReader,
    ICreatureTraitsQueryHandler creatureTraitsQueryHandler,
    ICombatCreatureStatesQueryHandler combatCreatureStatesQueryHandler) : IMonsterContextsQueryHandler
{
    public async Task<List<MonsterContext>> ExecuteAsync(GetMonsterContextsByExpeditionQuery byExpeditionQuery)
    {
        var monsters = (await creaturesReader.GetAliveMonstersAsync(byExpeditionQuery.ExpeditionId)).ToList();
        
        var monsterIds = monsters.Select(c => c.CreatureId).ToList();
        
        var traitsByMonsterId = await creatureTraitsQueryHandler.ExecuteAsync(
            new CreatureTraitsQuery() { CreatureIds = monsterIds });

        var statesByExpeditionId = await combatCreatureStatesQueryHandler.ExecuteAsync(
            new CharactersStatesQuery() { ExpeditionIds = [byExpeditionQuery.ExpeditionId] });

        var statesByMonsterId = statesByExpeditionId[byExpeditionQuery.ExpeditionId].ToDictionary(x => x.CreatureId);

        return monsters.ToEntities().ToMonsterContexts(traitsByMonsterId, statesByMonsterId);
    }
}