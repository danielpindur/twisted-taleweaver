using Dapper;
using Npgsql;
using TwistedTaleweaver.Common.CommandHandlers;
using TwistedTaleweaver.Common.DataAccess;
using TwistedTaleweaver.Common.DataAccess.Extensions;
using TwistedTaleweaver.Domain.Expeditions.Enums;
using TwistedTaleweaver.Integration.OpenAi.Adapters;
using TwistedTaleweaver.Integration.OpenAi.Expeditions;

namespace TwistedTaleweaver.Expeditions.CommandHandlers;

public class CreateExpeditionCommand 
{
    public string? Name { get; set; }
    
    public string? Description { get; set; }
    
    public ExpeditionDifficulty Difficulty { get; set; }
    
    public byte MaxParticipants { get; set; }
    
    public Guid UserId { get; set; }
}

public class CreateExpeditionResult
{
    public Guid ExpeditionId { get; set;}
}

public interface ICreateExpeditionCommandHandler : ICommandHandler<CreateExpeditionCommand, CreateExpeditionResult>
{
}

internal class CreateExpeditionCommandHandler(
    IGenerationAdapter generationAdapter,
    Func<IUnitOfWork> createUnitOfWork,
    IDbConnectionFactory connectionFactory) : ICreateExpeditionCommandHandler
{
    public async Task<CreateExpeditionResult> ExecuteAsync(CreateExpeditionCommand command)
    {
        var generatedExpedition = await generationAdapter.GenerateAsync(command.Difficulty, command.Name, command.Description);
        
        await using var unitOfWork = createUnitOfWork();

        return await unitOfWork.ExecuteInTransactionAsync(async transaction =>
        {
            var expeditionId = await CreateExpeditionAsync(command.UserId, generatedExpedition.Name, generatedExpedition.Description, command.Difficulty, command.MaxParticipants, transaction);
            await JoinExpeditionAsync(expeditionId, command.UserId, transaction);

            return new CreateExpeditionResult() { ExpeditionId = expeditionId };
        });
    }

    private async Task<Guid> CreateExpeditionAsync(
        Guid userId,
        string name,
        string description,
        ExpeditionDifficulty difficulty,
        int maxParticipants,
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

    private async Task JoinExpeditionAsync(
        Guid expeditionId,
        Guid userId,
        NpgsqlTransaction transaction)
    {
        await connectionFactory.ExecuteAsync(async (connection, tx) =>
        {
            const string sql = @"
                INSERT INTO expedition_participants 
                (expedition_id, user_id)
                VALUES 
                (@ExpeditionId, @UserId)";

            return await connection.ExecuteAsync(sql, new
            { 
                ExpeditionId = expeditionId,
                UserId = userId
            }, tx);
        }, transaction);
    }
}