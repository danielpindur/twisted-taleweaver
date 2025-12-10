using Dapper;
using TwistedTaleweaver.Common.DataAccess;
using TwistedTaleweaver.Common.DataAccess.Extensions;
using TwistedTaleweaver.Common.QueryHandlers;
using TwistedTaleweaver.Domain.Expeditions;

namespace TwistedTaleweaver.Expeditions.QueryHandlers;

public class ExpeditionByIdQuery
{
    public Guid ExpeditionId { get; set; }
}

public interface IExpeditionByIdQueryHandler : IQueryHandler<ExpeditionByIdQuery, Expedition?>
{
}

internal class ExpeditionByIdQueryHandler(
    IDbConnectionFactory connectionFactory,
    IExpeditionParticipantsQueryHandler expeditionParticipantsQueryHandler) : IExpeditionByIdQueryHandler
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
                    e.outcome_id AS outcome
                FROM expeditions e
                WHERE e.expedition_id = @ExpeditionId";

            return await connection.QuerySingleOrDefaultAsync<Expedition>(sql, new
            { 
                query.ExpeditionId
            }, tx);
        });

        var participantsTask = expeditionParticipantsQueryHandler.ExecuteAsync(
            new ExpeditionParticipantsQuery() { ExpeditionId = query.ExpeditionId });

        var expedition = await expeditionTask;

        if (expedition is null)
        {
            return null;
        }

        expedition.CurrentParticipants = (byte)(await participantsTask).Count;
        
        return expedition;
    }
}