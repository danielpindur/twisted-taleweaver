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
        int maxParticipants);
}

internal class ExpeditionsWriter(IUnitOfWork unitOfWork) : DatabaseAccessor(unitOfWork), IExpeditionsWriter
{
    public async Task<Guid> CreateAsync(
        Guid userId,
        string name,
        string description,
        ExpeditionDifficulty difficulty,
        int maxParticipants)
    {
        const string sql = @"
            INSERT INTO expeditions 
            (expedition_name, description, difficulty_id, max_participants, created_by_user_id)
            VALUES 
            (@Name, @Description, @Difficulty, @MaxParticipants, @UserId)
            RETURNING expedition_id";

        return await QuerySingleAsync<Guid>(sql, new
        { 
            Name = name,
            Description = description,
            Difficulty = difficulty,
            MaxParticipants = maxParticipants,
            UserId = userId
        });
    }
}