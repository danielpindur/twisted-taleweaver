using TwistedTaleweaver.DataAccess.Configuration;
using Microsoft.Extensions.Options;
using Npgsql;

namespace TwistedTaleweaver.DataAccess.Common;

internal interface IDbConnectionFactory
{
    NpgsqlConnection CreateConnection();
}

internal class DbConnectionFactory(IOptions<DataAccessConfiguration> configuration) : IDbConnectionFactory
{
    private readonly string _connectionString = configuration.Value.ConnectionString
        ?? throw new InvalidOperationException("DataAccess:ConnectionString is not configured.");

    public NpgsqlConnection CreateConnection()
    {
        return new NpgsqlConnection(_connectionString);
    }
}