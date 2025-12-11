using Dapper;
using Npgsql;
using TwistedTaleweaver.Core.Database;
using TwistedTaleweaver.Core.Database.Enums;

namespace TwistedTaleweaver.DataAccess.Common;

public abstract class DatabaseAccessor(IUnitOfWork unitOfWork) : IDatabaseAccessor
{
    private NpgsqlConnection GetConnection()
    {
        return unitOfWork.Connection as NpgsqlConnection
            ?? throw new InvalidOperationException("The database connection is not a valid NpgsqlConnection.");
    }

    private NpgsqlTransaction? GetTransaction(ConnectionMode connectionMode)
    {
        var transaction = unitOfWork.GetTransaction(connectionMode);

        if (transaction == null)
        {
            return null;
        }
        
        return transaction as NpgsqlTransaction
            ?? throw new InvalidOperationException("The database transaction is not a valid NpgsqlTransaction.");
    }
    
    public Task<T?> QuerySingleOrDefaultAsync<T>(string sql, object? param = null)
    {
        return GetConnection()
            .QuerySingleOrDefaultAsync<T>(sql, param, GetTransaction(ConnectionMode.ReadOnly));
    }

    public Task<T> QuerySingleAsync<T>(string sql, object? param = null)
    {
        return GetConnection()
            .QuerySingleAsync<T>(sql, param, GetTransaction(ConnectionMode.ReadOnly));
    }

    public Task<IEnumerable<T>> QueryAsync<T>(string sql, object? param = null)
    {
        return GetConnection()
            .QueryAsync<T>(sql, param, GetTransaction(ConnectionMode.ReadOnly));
    }

    public Task ExecuteAsync(string sql, object? param = null)
    {
        return GetConnection()
            .ExecuteAsync(sql, param, GetTransaction(ConnectionMode.ReadWrite));
    }

    public Task<T> ExecuteWithOutputAsync<T>(string sql, object? param = null)
    {
        return GetConnection()
            .QuerySingleAsync<T>(sql, param, GetTransaction(ConnectionMode.ReadWrite));
    }
}