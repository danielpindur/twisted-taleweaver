using TwistedTaleweaver.Core.Database;
using TwistedTaleweaver.Core.Pagination;
using TwistedTaleweaver.DataAccess.Common;
using TwistedTaleweaver.DataAccess.Expeditions.Dtos;
using TwistedTaleweaver.Domain.Expeditions;

namespace TwistedTaleweaver.DataAccess.Expeditions.Readers;

public interface IExpeditionsReader
{
    Task<Expedition?> GetByIdAsync(Guid expeditionId);
    
    Task<bool> ExistsAsync(Guid expeditionId);

    Task<IEnumerable<ExpeditionSlimWithCountDto>> GetPageAsync(Guid userId, int pageNumber, int pageSize);
}

internal class ExpeditionsReader(IUnitOfWorkProvider unitOfWorkProvider) : 
    DatabaseAccessor(unitOfWorkProvider), IExpeditionsReader
{
    public async Task<Expedition?> GetByIdAsync(Guid expeditionId)
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

        return await QuerySingleOrDefaultAsync<Expedition>(sql, new
        {
            ExpeditionId = expeditionId
        });
    }

    public async Task<bool> ExistsAsync(Guid expeditionId)
    {
        const string sql = @"
            SELECT EXISTS(
                SELECT 1 
                FROM expeditions ep 
                WHERE ep.expedition_id = @ExpeditionId)";

        return await QuerySingleAsync<bool>(sql, new
        {
            ExpeditionId = expeditionId
        });
    }

    public async Task<IEnumerable<ExpeditionSlimWithCountDto>> GetPageAsync(Guid userId, int pageNumber, int pageSize)
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

        return await QueryAsync<ExpeditionSlimWithCountDto>(sql, new
        {
            UserId = userId,
            PageSize = pageSize,
            Offset = PaginationHelper.CalculateOffset(pageNumber, pageSize)
        });
    }
}