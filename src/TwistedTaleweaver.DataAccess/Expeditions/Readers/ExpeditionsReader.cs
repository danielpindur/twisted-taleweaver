using TwistedTaleweaver.Core.Database;
using TwistedTaleweaver.Core.Pagination;
using TwistedTaleweaver.DataAccess.Common;
using TwistedTaleweaver.DataAccess.Expeditions.Dtos;

namespace TwistedTaleweaver.DataAccess.Expeditions.Readers;

public interface IExpeditionsReader
{
    Task<ExpeditionDto?> GetByIdAsync(Guid expeditionId);

    Task<bool> ExistsAsync(Guid expeditionId);

    Task<IEnumerable<ExpeditionDtoWithCount>> GetPageAsync(Guid userId, int pageNumber, int pageSize);
}

internal class ExpeditionsReader(IUnitOfWorkProvider unitOfWorkProvider) :
    DatabaseAccessor(unitOfWorkProvider), IExpeditionsReader
{
    public Task<ExpeditionDto?> GetByIdAsync(Guid expeditionId)
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
                u.user_id AS created_by_user_id,
                u.external_user_id AS created_by_external_user_id
            FROM expeditions e
            INNER JOIN users u ON e.created_by_user_id = u.user_id
            WHERE e.expedition_id = @ExpeditionId";

        return QuerySingleOrDefaultAsync<ExpeditionDto>(sql, new
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

    public Task<IEnumerable<ExpeditionDtoWithCount>> GetPageAsync(Guid userId, int pageNumber, int pageSize)
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
                u.user_id AS created_by_user_id,
                u.external_user_id AS created_by_external_user_id,
                COUNT(*) OVER() AS total_count
            FROM expeditions e
            INNER JOIN users u ON e.created_by_user_id = u.user_id
            WHERE e.created_by_user_id = @UserId OR EXISTS(SELECT 1 FROM expedition_participants ep WHERE ep.expedition_id = e.expedition_id AND ep.user_id = @UserId)
            ORDER BY e.created_at DESC
            LIMIT @PageSize OFFSET @Offset";

        return QueryAsync<ExpeditionDtoWithCount>(sql, new
        {
            UserId = userId,
            PageSize = pageSize,
            Offset = PaginationHelper.CalculateOffset(pageNumber, pageSize)
        });
    }
}