using Npgsql;

namespace TwistedTaleweaver.DataAccess.Common.Transactions.Extensions;

internal static class DbConnectionFactoryExtensions
{
    extension(IDbConnectionFactory connectionFactory)
    {
        public async Task<T> ExecuteAsync<T>(
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

        public async Task ExecuteAsync(
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
}