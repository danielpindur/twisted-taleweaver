using TwistedTaleweaver.Api.Expeditions.Entities;
using TwistedTaleweaver.Api.Expeditions.Queries;
using TwistedTaleweaver.Core.Database;
using TwistedTaleweaver.Core.Pagination;
using TwistedTaleweaver.DataAccess.Expeditions.Dtos;

namespace TwistedTaleweaver.DataAccess.Expeditions.Readers;

public interface IExpeditionsReader
{
    Task<Expedition?> GetByIdAsync(Guid expeditionId);

    Task<bool> ExistsAsync(Guid expeditionId);

    Task<IEnumerable<ExpeditionWithCount>> GetPageAsync(ExpeditionSearchQuery query);
    
    Task<IEnumerable<ExpeditionStatusDto>> GetStatusesAsync(List<Guid> expeditionIds);
    
    Task<ExpeditionStatistics?> GetStatisticsAsync(ExpeditionStatisticsQuery query);
}

internal class ExpeditionsReader(IUnitOfWorkProvider provider) : DatabaseAccessor(provider), IExpeditionsReader
{
    public Task<Expedition?> GetByIdAsync(Guid expeditionId)
    {
        const string sql = @"
            SELECT 
                e.expedition_id,
                e.expedition_name AS name,
                e.description,
                e.difficulty_id AS difficulty,
                e.min_participants,
                e.max_participants,
                e.lesson_learned,
                e.created_at,
                e.started_at,
                e.completed_at,
                (SELECT COUNT (*) FROM expedition_participants ep WHERE ep.expedition_id = e.expedition_id) AS current_participants,
                e.outcome_id AS outcome,
                e.created_by_user_id
            FROM expeditions e
            WHERE e.expedition_id = @ExpeditionId";

        return QuerySingleOrDefaultAsync<Expedition>(sql, new
        {
            ExpeditionId = expeditionId
        });
    }

    public Task<bool> ExistsAsync(Guid expeditionId)
    {
        const string sql = @"
            SELECT EXISTS(
                SELECT 1 
                FROM expeditions ep 
                WHERE ep.expedition_id = @ExpeditionId)";

        return QuerySingleAsync<bool>(sql, new
        {
            ExpeditionId = expeditionId
        });
    }

    public Task<IEnumerable<ExpeditionWithCount>> GetPageAsync(ExpeditionSearchQuery query)
    {
        const string sql = @"
            SELECT 
                e.expedition_id,
                e.expedition_name AS name,
                e.description,
                e.status_id AS status,
                e.difficulty_id AS difficulty,
                e.min_participants,
                e.max_participants,
                (SELECT COUNT (*) FROM expedition_participants ep WHERE ep.expedition_id = e.expedition_id) AS current_participants,
                e.outcome_id AS outcome,
                e.lesson_learned,
                e.created_by_user_id,
                e.created_at,
                e.started_at,
                e.completed_at,
                COUNT(*) OVER() AS total_count
            FROM expeditions e
            WHERE e.status_id = ANY(@Statuses)
                AND (@Difficulty IS NULL OR e.difficulty_id = @Difficulty)
                AND (@Outcome IS NULL OR e.outcome_id = @Outcome)
                AND (@SearchPattern IS NULL OR e.expedition_name LIKE @SearchPattern OR e.description LIKE @SearchPattern)
                AND (e.created_by_user_id = @UserId 
                    OR EXISTS(
                        SELECT 1 
                        FROM expedition_participants ep
                        WHERE ep.expedition_id = e.expedition_id AND ep.user_id = @UserId
                    ))
            ORDER BY e.created_at DESC
            LIMIT @PageSize OFFSET @Offset";

        return QueryAsync<ExpeditionWithCount>(sql, new
        {
            query.UserId,
            Statuses = query.Statuses.Select(x => (int)x).ToList(),
            query.Difficulty,
            query.Outcome,
            SearchPattern = query.SearchText is not null ? $"%{query.SearchText}%" : null,
            query.PageSize,
            Offset = PaginationHelper.CalculateOffset(query.PageNumber, query.PageSize)
        });
    }

    public Task<IEnumerable<ExpeditionStatusDto>> GetStatusesAsync(List<Guid> expeditionIds)
    {
        const string sql = @"
            SELECT 
                e.expedition_id,
                e.status_id AS status
            FROM expeditions e
            WHERE e.expedition_id = ANY(@ExpeditionIds)";

        return QueryAsync<ExpeditionStatusDto>(sql, new
        {
            ExpeditionIds = expeditionIds
        });
    }

    public Task<ExpeditionStatistics?> GetStatisticsAsync(ExpeditionStatisticsQuery query)
    {
        const string sql = @"
            SELECT 
                COUNT(*) FILTER (WHERE e.outcome_id = 1) AS successful_expeditions,
                COUNT(*) FILTER (WHERE e.outcome_id = 2) AS failed_expeditions,
                COUNT(*) AS total_expeditions
            FROM expeditions e
            WHERE e.status_id = ANY(@Statuses)
                AND (@Difficulty IS NULL OR e.difficulty_id = @Difficulty)
                AND (@Outcome IS NULL OR e.outcome_id = @Outcome)
                AND (@SearchPattern IS NULL OR e.expedition_name LIKE @SearchPattern OR e.description LIKE @SearchPattern)
                AND (e.created_by_user_id = @UserId 
                    OR EXISTS(
                        SELECT 1 
                        FROM expedition_participants ep 
                        WHERE ep.expedition_id = e.expedition_id AND ep.user_id = @UserId
                    ))
            GROUP BY e.expedition_id";

        return QuerySingleOrDefaultAsync<ExpeditionStatistics>(sql, new
        {
            query.UserId,
            Statuses = query.Statuses.Select(x => (int)x).ToList(),
            query.Difficulty,
            query.Outcome,
            SearchPattern = query.SearchText is not null ? $"%{query.SearchText}%" : null
        });
    }
}