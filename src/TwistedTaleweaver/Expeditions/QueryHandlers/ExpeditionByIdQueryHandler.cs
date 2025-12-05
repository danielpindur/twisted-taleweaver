using Dapper;
using TwistedTaleweaver.Common.DataAccess;
using TwistedTaleweaver.Common.DataAccess.Extensions;
using TwistedTaleweaver.Common.QueryHandlers;
using TwistedTaleweaver.Expeditions.Entities;

namespace TwistedTaleweaver.Expeditions.QueryHandlers;

public class ExpeditionByIdQuery
{
    public Guid ExpeditionId { get; set; }
}

public interface IExpeditionByIdQueryHandler : IQueryHandler<ExpeditionByIdQuery, Expedition?>
{
}

internal class ExpeditionByIdQueryHandler(IDbConnectionFactory connectionFactory) : IExpeditionByIdQueryHandler
{
    public async Task<Expedition?> ExecuteAsync(ExpeditionByIdQuery query)
    {
        var expeditionTask = connectionFactory.ExecuteAsync(async (connection, tx) =>
        {
            const string sql = @"
                SELECT 
                    e.expedition_id,
                    e.expedition_name AS name,
                    e.description,
                    e.status_id AS status,
                    e.difficulty_id AS difficulty,
                    e.max_participants,
                    e.outcome_id AS outcome,
                    e.created_by_user_id,
                    e.created_at,
                    e.started_at,
                    e.completed_at,
                    e.updated_at
                FROM expeditions e
                WHERE e.expedition_id = @ExpeditionId";

            return await connection.QuerySingleOrDefaultAsync<Expedition>(sql, new
            { 
                query.ExpeditionId
            }, tx);
        });
        
        var participantsTask = connectionFactory.ExecuteAsync(async (connection, tx) =>
        {
            const string sql = @"
                SELECT 
                    ep.user_id
                FROM expedition_participants ep
                WHERE ep.expedition_id = @ExpeditionId";

            return await connection.QueryAsync<ExpeditionParticipant>(sql, new
            { 
                query.ExpeditionId
            }, tx);
        });

        var expedition = await expeditionTask;

        if (expedition is null)
        {
            return null;
        }

        expedition.Participants = (await participantsTask).ToList();
        
        return expedition;
    }
}