using TwistedTaleweaver.Api.Expeditions.Entities;
using TwistedTaleweaver.Api.Expeditions.Entities.Enums;
using TwistedTaleweaver.Api.Expeditions.Queries;
using TwistedTaleweaver.Creatures.Mappers;
using TwistedTaleweaver.DataAccess.Creatures.Readers;
using TwistedTaleweaver.DataAccess.Expeditions.Readers;

namespace TwistedTaleweaver.Expeditions.QueryHandlers;

internal class ExpeditionStatesQueryHandler(
    IExpeditionsReader expeditionsReader,
    ICreaturesReader creaturesReader,
    IExpeditionTurnOrdersReader expeditionTurnOrdersReader) : IExpeditionStatesQueryHandler
{
    public async Task<IEnumerable<ExpeditionState>> ExecuteAsync(ExpeditionStatesQuery query)
    {
        var statuses = await expeditionsReader.GetStatusesAsync(query.ExpeditionIds);
        var turnOrders = await expeditionTurnOrdersReader.GetByExpeditionsAsync(query.ExpeditionIds);
        var monsters = await creaturesReader.GetMonstersAsync(query.ExpeditionIds);
        
        var statusByExpeditionId = statuses.ToDictionary(s => s.ExpeditionId);
        var turnOrderByExpeditionId = turnOrders.ToDictionary(t => t.ExpeditionId);
        var monstersByExpeditionId = monsters
            .GroupBy(x => x.ExpeditionId)
            .ToDictionary(x => x.Key, y => y.ToList());
            
        var result = new List<ExpeditionState>();
        foreach (var expeditionId in query.ExpeditionIds)
        {
            statusByExpeditionId.TryGetValue(expeditionId, out var status);
            turnOrderByExpeditionId.TryGetValue(expeditionId, out var turnOrder);
            monstersByExpeditionId.TryGetValue(expeditionId, out var expeditionMonsters);

            result.Add(new ExpeditionState()
            {
                ExpeditionId = expeditionId,
                Status = status?.Status ?? ExpeditionStatus.Created,
                ActiveCreatureId = turnOrder?.CurrentActorId,
                Monsters =  expeditionMonsters.ToEntities()
            });
        }

        return result;
    }

    public async Task<ExpeditionState> ExecuteAsync(ExpeditionStateQuery query)
    {
        var expeditionStateById = await ExecuteAsync(
            new ExpeditionStatesQuery() { ExpeditionIds = [query.ExpeditionId] });

        return expeditionStateById.Single();
    }
}