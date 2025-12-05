using Dapper;
using TwistedTaleweaver.Common.DataAccess;
using TwistedTaleweaver.Common.DataAccess.Extensions;
using TwistedTaleweaver.Common.Pagination.Entities;
using TwistedTaleweaver.Common.Pagination.QueryHandlers;
using TwistedTaleweaver.Common.QueryHandlers;
using TwistedTaleweaver.Expeditions.Entities;

namespace TwistedTaleweaver.Expeditions.QueryHandlers;

public class ExpeditionSearchQuery : PaginationQuery
{
    public Guid UserId { get; set; }
}

public interface IExpeditionSearchQueryHandler : IQueryHandler<ExpeditionSearchQuery, PaginatedResult<Expedition>>
{
}

internal class ExpeditionSearchQueryHandler(IDbConnectionFactory connectionFactory) : IExpeditionSearchQueryHandler
{
    public async Task<PaginatedResult<Expedition>> ExecuteAsync(ExpeditionSearchQuery query)
    {
        var dtos = (await connectionFactory.ExecuteAsync(async (connection, tx) =>
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
                    e.updated_at,
                    COUNT(*) OVER() AS total_count
                FROM expeditions e
                WHERE e.created_by_user_id = @UserId
                ORDER BY e.created_at DESC
                LIMIT @PageSize OFFSET @Offset";

            return await connection.QueryAsync<ExpeditionWithCount>(sql, new
            {
                query.UserId,
                query.PageSize,
                query.Offset
            }, tx);
        })).ToList();
        
        var participants = await connectionFactory.ExecuteAsync(async (connection, tx) =>
        {
            const string sql = @"
                SELECT 
                    ep.expedition_id,
                    ep.user_id
                FROM expedition_participants ep
                WHERE ep.expedition_id = ANY(@ExpeditionIds)";

            return await connection.QueryAsync<ExpeditionParticipantDto>(sql, new
            { 
                ExpeditionIds = dtos.Select(ep => ep.ExpeditionId).ToList()
            }, tx);
        });
        
        var participantsByExpeditionId = participants
            .GroupBy(x => x.ExpeditionId)
            .ToDictionary(x => x.Key, x => x.ToList());

        foreach (var dto in dtos)
        {
            dto.Participants = participantsByExpeditionId.TryGetValue(dto.ExpeditionId, out var expeditionParticipants) 
                ? expeditionParticipants.Select(x => x as ExpeditionParticipant).ToList() 
                : new List<ExpeditionParticipant>();
        }

        return new PaginatedResult<Expedition>(dtos, dtos.FirstOrDefault()?.TotalCount ?? 0, query);
    }

    private class ExpeditionWithCount : Expedition
    {
        public int TotalCount { get; set; }
    }

    private class ExpeditionParticipantDto : ExpeditionParticipant
    {
        public Guid ExpeditionId { get; set; }
    }
}