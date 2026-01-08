using TwistedTaleweaver.Core.Database;
using TwistedTaleweaver.DataAccess.Expeditions.Dtos;

namespace TwistedTaleweaver.DataAccess.Expeditions.Readers;

public interface IExpeditionParticipantsReader
{
    Task<IEnumerable<ExpeditionParticipantDto>> GetByExpeditionsAsync(List<Guid> expeditionIds);

    Task<bool> ExistsAsync(Guid expeditionId, Guid userId);
    
    Task<ExpeditionParticipantDto> GetForExpeditionByUserAsync(Guid expeditionId, Guid userId);
}

internal class ExpeditionParticipantsReader(IUnitOfWorkProvider provider) : DatabaseAccessor(provider), IExpeditionParticipantsReader
{
    public Task<IEnumerable<ExpeditionParticipantDto>> GetByExpeditionsAsync(List<Guid> expeditionIds)
    {
        const string sql = @"
            SELECT 
                ep.expedition_id,
                ep.character_id,
                ep.user_id,
                u.external_user_id
            FROM expedition_participants ep
            INNER JOIN users u ON u.user_id = ep.user_id
            WHERE ep.expedition_id = ANY(@ExpeditionIds)";

        return QueryAsync<ExpeditionParticipantDto>(sql, new
        {
            ExpeditionIds = expeditionIds
        });
    }

    public Task<bool> ExistsAsync(Guid expeditionId, Guid userId)
    {
        const string sql = @"
            SELECT EXISTS(
                SELECT 1 
                FROM expedition_participants ep 
                WHERE ep.expedition_id = @ExpeditionId 
                  AND ep.user_id = @UserId)";

        return QuerySingleAsync<bool>(sql, new
        {
            ExpeditionId = expeditionId,
            UserId = userId
        });
    }

    public Task<ExpeditionParticipantDto> GetForExpeditionByUserAsync(Guid expeditionId, Guid userId)
    {
        const string sql = @"
            SELECT 
                ep.expedition_id,
                ep.character_id,
                ep.user_id,
                u.external_user_id
            FROM expedition_participants ep
            INNER JOIN users u ON u.user_id = ep.user_id
            WHERE ep.expedition_id = @ExpeditionId AND ep.user_id = @UserId";

        return QuerySingleAsync<ExpeditionParticipantDto>(sql, new
        {
            ExpeditionId = expeditionId,
            UserId = userId
        });
    }
}