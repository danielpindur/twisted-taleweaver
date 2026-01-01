using TwistedTaleweaver.Api.Expeditions.Entities;
using TwistedTaleweaver.Core.Database;
using TwistedTaleweaver.Core.Pagination;
using TwistedTaleweaver.DataAccess.Common;
using TwistedTaleweaver.DataAccess.Expeditions.Dtos;

namespace TwistedTaleweaver.DataAccess.Expeditions.Readers;

public interface IExpeditionsReader
{
    Task<Expedition?> GetByIdAsync(Guid expeditionId);

    Task<bool> ExistsAsync(Guid expeditionId);

    Task<IEnumerable<ExpeditionWithCount>> GetPageAsync(Guid userId, int pageNumber, int pageSize);
    
    Task<IEnumerable<ExpeditionStatusDto>> GetStatusesAsync(List<Guid> expeditionIds);
}

internal class ExpeditionsReader(IUnitOfWorkProvider unitOfWorkProvider) :
    DatabaseAccessor(unitOfWorkProvider), IExpeditionsReader
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

    public Task<IEnumerable<ExpeditionWithCount>> GetPageAsync(Guid userId, int pageNumber, int pageSize)
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
                e.created_by_user_id,
                COUNT(*) OVER() AS total_count
            FROM expeditions e
            WHERE e.created_by_user_id = @UserId OR EXISTS(SELECT 1 FROM expedition_participants ep WHERE ep.expedition_id = e.expedition_id AND ep.user_id = @UserId)
            ORDER BY e.created_at DESC
            LIMIT @PageSize OFFSET @Offset";

        return QueryAsync<ExpeditionWithCount>(sql, new
        {
            UserId = userId,
            PageSize = pageSize,
            Offset = PaginationHelper.CalculateOffset(pageNumber, pageSize)
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
}