using Npgsql;

namespace TwistedTaleweaver.DataAccess.Common.Extensions;

internal static class DbConnectionFactoryExtensions
{
    public static async Task<T> ExecuteAsync<T>(
        this IDbConnectionFactory connectionFactory,
        Func<NpgsqlConnection, NpgsqlTransaction?, Task<T>> operation,
        NpgsqlTransaction? transaction = null)
    {
        if (transaction is not null)
        {
            return await operation(transaction.Connection!, transaction);
        }

        await using var connection = connectionFactory.CreateConnection();
        await connection.OpenAsync();
        return await operation(connection, null);
    }

    public static async Task ExecuteAsync(
        this IDbConnectionFactory connectionFactory,
        Func<NpgsqlConnection, NpgsqlTransaction?, Task> operation,
        NpgsqlTransaction? transaction = null)
    {
        if (transaction is not null)
        {
            await operation(transaction.Connection!, transaction);
            return;
        }

        await using var connection = connectionFactory.CreateConnection();
        await connection.OpenAsync();
        await operation(connection, null);
    }
}