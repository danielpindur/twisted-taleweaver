using TwistedTaleweaver.Api.Expeditions.Entities;
using TwistedTaleweaver.Core.Database;

namespace TwistedTaleweaver.DataAccess.Expeditions.Writers;

public interface IExpeditionTurnOrdersWriter
{
    Task Save(ExpeditionTurnOrder turnOrder);
}

internal class ExpeditionTurnOrdersWriter(IUnitOfWorkProvider provider) : DatabaseAccessor(provider), IExpeditionTurnOrdersWriter
{
    public Task Save(ExpeditionTurnOrder turnOrder)
    {
        const string sql = @"
            INSERT INTO expedition_turn_orders (
                expedition_id,
                current_turn_index,
                awaiting_action,
                turns
            )
            VALUES (
                @ExpeditionId,
                @CurrentTurnIndex,
                @AwaitingAction,
                @Turns::jsonb
            )
            ON CONFLICT (expedition_id)
            DO UPDATE SET
                current_turn_index = EXCLUDED.current_turn_index,
                awaiting_action    = EXCLUDED.awaiting_action,
                turns              = EXCLUDED.turns";

        return ExecuteAsync(sql, new
        {
            turnOrder.ExpeditionId,
            turnOrder.CurrentTurnIndex,
            turnOrder.AwaitingAction,
            turnOrder.Turns
        });
    }
}