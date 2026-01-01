using TwistedTaleweaver.Api.Expeditions.Entities;
using TwistedTaleweaver.Core.Database;
using TwistedTaleweaver.DataAccess.Common;

namespace TwistedTaleweaver.DataAccess.Expeditions.Readers;

public interface IExpeditionTurnOrdersReader
{
    Task<ExpeditionTurnOrder> GetByExpeditionAsync(Guid expeditionId);
    
    Task<IEnumerable<ExpeditionTurnOrder>> GetByExpeditionsAsync(List<Guid> expeditionIds);
}

internal class ExpeditionTurnOrdersReader(IUnitOfWorkProvider provider) : DatabaseAccessor(provider), IExpeditionTurnOrdersReader
{
    public Task<ExpeditionTurnOrder> GetByExpeditionAsync(Guid expeditionId)
    {
        const string sql = @"
            SELECT 
                eto.expedition_id,
                eto.current_turn_index,
                eto.awaiting_action,
                eto.turns
            FROM expedition_turn_orders eto
            WHERE eto.expedition_id = @ExpeditionId";

        return QuerySingleAsync<ExpeditionTurnOrder>(sql, new
        {
            ExpeditionId = expeditionId
        });
    }

    public Task<IEnumerable<ExpeditionTurnOrder>> GetByExpeditionsAsync(List<Guid> expeditionIds)
    {
        const string sql = @"
            SELECT 
                eto.expedition_id,
                eto.current_turn_index,
                eto.awaiting_action,
                eto.turns
            FROM expedition_turn_orders eto
            WHERE eto.expedition_id = ANY(@ExpeditionIds)";

        return QueryAsync<ExpeditionTurnOrder>(sql, new
        {
            ExpeditionIds = expeditionIds
        });
    }
}