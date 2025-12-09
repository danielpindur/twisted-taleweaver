using Dapper;
using FluentValidation;
using Npgsql;
using TwistedTaleweaver.Common.CommandHandlers;
using TwistedTaleweaver.Common.DataAccess;
using TwistedTaleweaver.Common.DataAccess.Extensions;
using TwistedTaleweaver.Expeditions.Entities;

namespace TwistedTaleweaver.Expeditions.CommandHandlers;

public class JoinExpeditionCommand
{
    public Guid ExpeditionId { get; set; }
    
    public Guid UserId { get; set; }
}

public interface IJoinExpeditionCommandHandler : ICommandHandler<JoinExpeditionCommand>
{
}

internal class JoinExpeditionCommandHandler(
    Func<IUnitOfWork> createUnitOfWork,
    IDbConnectionFactory connectionFactory) : IJoinExpeditionCommandHandler
{
    public async Task ExecuteAsync(JoinExpeditionCommand command)
    {
        if (!await ExpeditionExistsAsync(command))
        {
            throw new ValidationException("Expedition does not exist.");
        }
        
        if (await HasAlreadyJoinedAsync(command))
        {
            throw new ValidationException("User has already joined the expedition.");
        }
        
        await using var unitOfWork = createUnitOfWork();

        await unitOfWork.ExecuteInTransactionAsync(async transaction =>
        {
            await JoinExpeditionAsync(command.ExpeditionId, command.UserId, transaction);
        });
    }
    
    private async Task<bool> HasAlreadyJoinedAsync(JoinExpeditionCommand command)
    {
        return await connectionFactory.ExecuteAsync(async (connection, tx) =>
        {
            const string sql = @"
                SELECT EXISTS(
                    SELECT 1 
                    FROM expedition_participants ep 
                    WHERE ep.expedition_id = @ExpeditionId 
                      AND ep.user_id = @UserId)";

            return await connection.QuerySingleAsync<bool>(sql, new
            { 
                command.ExpeditionId,
                command.UserId
            }, tx);
        });
    }

    private async Task<bool> ExpeditionExistsAsync(JoinExpeditionCommand command)
    {
        return await connectionFactory.ExecuteAsync(async (connection, tx) =>
        {
            const string sql = @"
                SELECT EXISTS(
                    SELECT 1 
                    FROM expeditions ep 
                    WHERE ep.expedition_id = @ExpeditionId)";

            return await connection.QuerySingleAsync<bool>(sql, new
            { 
                command.ExpeditionId
            }, tx);
        });
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