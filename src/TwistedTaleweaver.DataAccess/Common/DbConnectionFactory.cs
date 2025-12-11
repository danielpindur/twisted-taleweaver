using System.Data.Common;
using Microsoft.Extensions.Configuration;
using Npgsql;
using TwistedTaleweaver.Core.Database;

namespace TwistedTaleweaver.DataAccess.Common;

internal class DbConnectionFactory(IConfiguration configuration) : IDbConnectionFactory
{
    private readonly string _connectionString = configuration.GetConnectionString("Database")
                                                ?? throw new InvalidOperationException("Database ConnectionString is not configured.");

    public DbConnection CreateConnection()
    {
        return new NpgsqlConnection(_connectionString);
    }
}
