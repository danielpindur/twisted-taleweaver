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
                    e.expedition_name,
                    e.description,
                    e.status_id,
                    e.difficulty_id,
                    e.max_participants,
                    e.outcome_id,
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

        return new PaginatedResult<Expedition>(dtos, dtos.FirstOrDefault()?.TotalCount ?? 0, query);
    }

    private class ExpeditionWithCount : Expedition
    {
        public int TotalCount { get; set; }
    }
}