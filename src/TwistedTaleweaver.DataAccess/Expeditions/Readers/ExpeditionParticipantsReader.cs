using TwistedTaleweaver.Core.Database;
using TwistedTaleweaver.DataAccess.Common;
using TwistedTaleweaver.DataAccess.Expeditions.Dtos;

namespace TwistedTaleweaver.DataAccess.Expeditions.Readers;

public interface IExpeditionParticipantsReader
{
    Task<IEnumerable<ExpeditionParticipantDto>> GetByExpeditionAsync(Guid expeditionId);

    Task<bool> ExistsAsync(Guid expeditionId, Guid userId);
}

internal class ExpeditionParticipantsReader(IUnitOfWorkProvider provider) : DatabaseAccessor(provider), IExpeditionParticipantsReader
{
    public Task<IEnumerable<ExpeditionParticipantDto>> GetByExpeditionAsync(Guid expeditionId)
    {
        const string sql = @"
            SELECT 
                ep.expedition_id,
                ep.character_id,
                ep.user_id,
                u.external_user_id
            FROM expedition_participants ep
            INNER JOIN users u ON u.user_id = ep.user_id
            WHERE ep.expedition_id = @ExpeditionId";

        return QueryAsync<ExpeditionParticipantDto>(sql, new
        {
            ExpeditionId = expeditionId
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
}