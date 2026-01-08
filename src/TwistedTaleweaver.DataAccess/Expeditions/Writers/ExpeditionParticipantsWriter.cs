using TwistedTaleweaver.Core.Database;

namespace TwistedTaleweaver.DataAccess.Expeditions.Writers;

public interface IExpeditionParticipantsWriter
{
    Task AddAsync(Guid expeditionId, Guid userId);

    Task SetCharacterIdAsync(Guid expeditionId, Guid userId, Guid characterId);
}

internal class ExpeditionParticipantsWriter(IUnitOfWorkProvider provider) : DatabaseAccessor(provider), IExpeditionParticipantsWriter
{
    public Task AddAsync(Guid expeditionId, Guid userId)
    {
        const string sql = @"
            INSERT INTO expedition_participants 
            (expedition_id, user_id)
            VALUES 
            (@ExpeditionId, @UserId)";

        return ExecuteAsync(sql, new
        {
            ExpeditionId = expeditionId,
            UserId = userId
        });
    }

    public Task SetCharacterIdAsync(Guid expeditionId, Guid userId, Guid characterId)
    {
        const string sql = @"
            UPDATE expedition_participants 
            SET character_id = @CharacterId
            WHERE expedition_id = @ExpeditionId AND user_id = @UserId";

        return ExecuteAsync(sql, new
        {
            ExpeditionId = expeditionId,
            UserId = userId,
            CharacterId = characterId
        });
    }
}