using TwistedTaleweaver.Core.Database;
using TwistedTaleweaver.DataAccess.Common;
using TwistedTaleweaver.Domain.Expeditions.Enums;

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
}

internal class ExpeditionsWriter(IUnitOfWorkProvider unitOfWorkProvider) : 
    DatabaseAccessor(unitOfWorkProvider), IExpeditionsWriter
{
    public async Task<Guid> CreateAsync(
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

        return await QuerySingleAsync<Guid>(sql, new
        { 
            Name = name,
            Description = description,
            Difficulty = difficulty,
            MinParticipants = minParticipants,
            MaxParticipants = maxParticipants,
            UserId = userId
        });
    }
}