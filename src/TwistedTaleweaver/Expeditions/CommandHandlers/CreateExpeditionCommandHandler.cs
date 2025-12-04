using Dapper;
using Npgsql;
using TwistedTaleweaver.Common.CommandHandlers;
using TwistedTaleweaver.Common.DataAccess;
using TwistedTaleweaver.Common.DataAccess.Extensions;
using TwistedTaleweaver.Expeditions.Entities.Enums;

namespace TwistedTaleweaver.Expeditions.CommandHandlers;

public class CreateExpeditionCommand 
{
    public string? Name { get; set; }
    
    public string? Description { get; set; }
    
    public ExpeditionDifficulty Difficulty { get; set; }
    
    public byte MaxParticipants { get; set; }
    
    public required string ExternalUserId { get; set; }
}

public class CreateExpeditionResult
{
    public Guid ExpeditionId { get; set;}
}

public interface ICreateExpeditionCommandHandler : ICommandHandler<CreateExpeditionCommand, CreateExpeditionResult>
{
}

internal class CreateExpeditionCommandHandler(
    Func<IUnitOfWork> createUnitOfWork,
    IDbConnectionFactory connectionFactory) : ICreateExpeditionCommandHandler
{
    public async Task<CreateExpeditionResult> ExecuteAsync(CreateExpeditionCommand command)
    {
        if (string.IsNullOrEmpty(command.Name))
        {
            // TODO: Actual generation
            command.Name = "GeneratedName";
        }

        if (string.IsNullOrEmpty(command.Description))
        {
            // TODO: Actual generation
            command.Description = "GeneratedDescription";
        }
        
        await using var unitOfWork = createUnitOfWork();

        return await unitOfWork.ExecuteInTransactionAsync(async transaction =>
        {
            var userId = await GetUserId(command.ExternalUserId, transaction);
            var expeditionId = await CreateExpedition(userId, command.Name, command.Description, command.Difficulty, command.MaxParticipants, transaction);

            return new CreateExpeditionResult() { ExpeditionId = expeditionId };
        });
    }

    private async Task<Guid> GetUserId(string externalUserId, NpgsqlTransaction transaction)
    {
        return await connectionFactory.ExecuteAsync(async (connection, tx) =>
        {
            const string sql = @"
                IF NOT EXISTS(SELECT 1 FROM users WHERE external_user_id = @ExternalUserId)
                BEGIN
                    INSERT INTO users (external_user_id)
                    VALUES (@ExternalUserId)
                END

                SELECT user_id
                FROM users
                WHERE external_user_id = @ExternalUserId";

            return await connection.QuerySingleAsync<Guid>(sql, new
            { 
                ExternalUserId = externalUserId
            }, tx);
        }, transaction);
    }

    private async Task<Guid> CreateExpedition(
        Guid userId,
        string name,
        string description,
        ExpeditionDifficulty difficulty,
        byte maxParticipants,
        NpgsqlTransaction transaction)
    {
        return await connectionFactory.ExecuteAsync(async (connection, tx) =>
        {
            const string sql = @"
                INSERT INTO expeditions 
                (expedition_name, description, difficulty_id, max_participants, created_by_user_id)
                VALUES 
                (@Name, @Description, @Difficulty, @MaxParticipants, @UserId)
                RETURNING expedition_id";

            return await connection.QuerySingleAsync<Guid>(sql, new
            { 
                Name = name,
                Description = description,
                Difficulty = difficulty,
                MaxParticipants = maxParticipants,
                UserId = userId
            }, tx);
        }, transaction);
    }
}