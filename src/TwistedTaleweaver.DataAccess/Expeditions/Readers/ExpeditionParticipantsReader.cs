using TwistedTaleweaver.Core.Database;
using TwistedTaleweaver.DataAccess.Common;
using TwistedTaleweaver.DataAccess.Expeditions.Dtos;

namespace TwistedTaleweaver.DataAccess.Expeditions.Readers;

public interface IExpeditionParticipantsReader
{
    Task<IEnumerable<ExpeditionParticipantDto>> GetByExpeditionAsync(Guid expeditionId);
    
    Task<IEnumerable<ExpeditionParticipantDto>> GetByExpeditionsAsync(List<Guid> expeditionIds);
    
    Task<bool> ExistsAsync(Guid expeditionId, Guid userId);
}

internal class ExpeditionParticipantsReader(IUnitOfWork unitOfWork)
    : DatabaseAccessor(unitOfWork), IExpeditionParticipantsReader
{
    public async Task<IEnumerable<ExpeditionParticipantDto>> GetByExpeditionAsync(Guid expeditionId)
    {
        const string sql = @"
            SELECT 
                ep.expedition_id,
                ep.user_id,
                u.external_user_id
            FROM expedition_participants ep
            JOIN users u ON u.user_id = ep.user_id
            WHERE ep.expedition_id = @ExpeditionId";

        return await QueryAsync<ExpeditionParticipantDto>(sql, new
        {
            ExpeditionId = expeditionId
        });
    }
    
    public async Task<IEnumerable<ExpeditionParticipantDto>> GetByExpeditionsAsync(List<Guid> expeditionIds)
    {
        const string sql = @"
            SELECT 
                ep.expedition_id,
                ep.user_id,
                u.external_user_id
            FROM expedition_participants ep
            JOIN users u ON u.user_id = ep.user_id
            WHERE ep.expedition_id = ANY(@ExpeditionIds)";

        return await QueryAsync<ExpeditionParticipantDto>(sql, new
        {
            ExpeditionIds = expeditionIds
        });
    }

    public async Task<bool> ExistsAsync(Guid expeditionId, Guid userId)
    {
        const string sql = @"
            SELECT EXISTS(
                SELECT 1 
                FROM expedition_participants ep 
                WHERE ep.expedition_id = @ExpeditionId 
                  AND ep.user_id = @UserId)";

        return await QuerySingleAsync<bool>(sql, new
        { 
            ExpeditionId = expeditionId,
            UserId = userId
        });
    }
}