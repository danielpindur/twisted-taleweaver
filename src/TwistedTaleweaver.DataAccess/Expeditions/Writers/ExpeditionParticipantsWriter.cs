using TwistedTaleweaver.Core.Database.UnitOfWork;
using TwistedTaleweaver.DataAccess.Common;

namespace TwistedTaleweaver.DataAccess.Expeditions.Writers;

public interface IExpeditionParticipantsWriter
{
    Task AddAsync(Guid expeditionId, Guid userId);
}

internal class ExpeditionParticipantsWriter(IUnitOfWork unitOfWork) : DatabaseAccessor(unitOfWork), IExpeditionParticipantsWriter
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