using TwistedTaleweaver.Core.Database;
using TwistedTaleweaver.DataAccess.Common;

namespace TwistedTaleweaver.DataAccess.Expeditions.Writers;

public interface IExpeditionParticipantsWriter
{
    Task AddAsync(Guid expeditionId, Guid userId);
}

internal class ExpeditionParticipantsWriter(IUnitOfWorkProvider unitOfWorkProvider) : 
    DatabaseAccessor(unitOfWorkProvider), IExpeditionParticipantsWriter
{
    public async Task AddAsync(Guid expeditionId, Guid userId)
    {
        const string sql = @"
            INSERT INTO expedition_participants 
            (expedition_id, user_id)
            VALUES 
            (@ExpeditionId, @UserId)";

        await ExecuteAsync(sql, new
        {
            ExpeditionId = expeditionId,
            UserId = userId
        });
    }
}