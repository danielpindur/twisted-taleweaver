using TwistedTaleweaver.Api.Expeditions.Entities.Enums;
using TwistedTaleweaver.Core.Database;
using TwistedTaleweaver.DataAccess.Common;

namespace TwistedTaleweaver.DataAccess.Expeditions.Writers;

public interface IExpeditionsWriter
{
    Task<Guid> CreateAsync(
        Guid userId,
        string name,
        string description,
        ExpeditionDifficulty difficulty,
        int minParticipants,
        int maxParticipants);

    Task StartAsync(Guid expeditionId);
}

internal class ExpeditionsWriter(IUnitOfWorkProvider provider) : DatabaseAccessor(provider), IExpeditionsWriter
{
    public Task<Guid> CreateAsync(
        Guid userId,
        string name,
        string description,
        ExpeditionDifficulty difficulty,
        int minParticipants,
        int maxParticipants)
    {
        const string sql = @"
            INSERT INTO expeditions 
            (expedition_name, description, difficulty_id, min_participants, max_participants, created_by_user_id)
            VALUES 
            (@Name, @Description, @Difficulty, @MinParticipants, @MaxParticipants, @UserId)
            RETURNING expedition_id";

        return ExecuteWithOutputAsync<Guid>(sql, new
        {
            Name = name,
            Description = description,
            Difficulty = difficulty,
            MinParticipants = minParticipants,
            MaxParticipants = maxParticipants,
            UserId = userId
        });
    }

    public Task StartAsync(Guid expeditionId)
    {
        const string sql = @"
            UPDATE expeditions 
            SET status_id = @Status, started_at = CURRENT_TIMESTAMP
            WHERE expedition_id = @ExpeditionId";

        return ExecuteAsync(sql, new
        {
            ExpeditionId = expeditionId,
            Status = ExpeditionStatus.Started
        });
    }
}