using Dapper;
using TwistedTaleweaver.Common.CommandHandlers;
using TwistedTaleweaver.Common.DataAccess;
using TwistedTaleweaver.Common.DataAccess.Extensions;
using TwistedTaleweaver.Users.Entities;

namespace TwistedTaleweaver.Users.CommandHandlers;

public class GetOrCreateUserCommand
{
    public required string ExternalUserId { get; set; }
}

public interface IGetOrCreateUserCommandHandler : ICommandHandler<GetOrCreateUserCommand, User>
{
}

internal class GetOrCreateUserCommandHandler(IDbConnectionFactory connectionFactory) : IGetOrCreateUserCommandHandler
{
    public async Task<User> ExecuteAsync(GetOrCreateUserCommand command)
    {
        return await connectionFactory.ExecuteAsync(async (connection, tx) =>
        {
            const string sql = @"
                INSERT INTO users (external_user_id)
                VALUES ('user_31MpMWGa4Xq8pxD4wbk3F8Hehd2')
                ON CONFLICT (external_user_id) DO UPDATE
                    SET external_user_id = EXCLUDED.external_user_id
                RETURNING user_id, external_user_id";

            return await connection.QuerySingleAsync<User>(sql, new
            { 
                command.ExternalUserId
            }, tx);
        });
    }
}