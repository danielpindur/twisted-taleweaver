using Microsoft.Extensions.Configuration;
using Npgsql;

namespace TwistedTaleweaver.Common.DataAccess;

internal interface IDbConnectionFactory
{
    NpgsqlConnection CreateConnection();
}

internal class DbConnectionFactory(IConfiguration configuration) : IDbConnectionFactory
{
    private readonly string _connectionString = configuration.GetConnectionString("Database")
                                                ?? throw new InvalidOperationException("Database ConnectionString is not configured.");

    public NpgsqlConnection CreateConnection()
    {
        return new NpgsqlConnection(_connectionString);
    }
}
