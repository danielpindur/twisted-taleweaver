using Dapper;
using Npgsql;
using TwistedTaleweaver.Core.Database;
using TwistedTaleweaver.Core.Database.Enums;

namespace TwistedTaleweaver.DataAccess.Common;

public abstract class DatabaseAccessor(IUnitOfWorkProvider unitOfWorkProvider) : IDatabaseAccessor
{
    private NpgsqlConnection GetConnection()
    {
        return unitOfWorkProvider.GetActiveUnitOfWork().Connection as NpgsqlConnection
               ?? throw new InvalidOperationException("The database connection is not a valid NpgsqlConnection.");
    }

    private NpgsqlTransaction? GetTransaction(TransactionMode mode)
    {
        var transaction = unitOfWorkProvider.GetActiveUnitOfWork().GetTransaction(mode);

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
            .QuerySingleOrDefaultAsync<T>(sql, param, GetTransaction(TransactionMode.ReadOnly));
    }

    public Task<T> QuerySingleAsync<T>(string sql, object? param = null)
    {
        return GetConnection()
            .QuerySingleAsync<T>(sql, param, GetTransaction(TransactionMode.ReadOnly));
    }

    public Task<IEnumerable<T>> QueryAsync<T>(string sql, object? param = null)
    {
        return GetConnection()
            .QueryAsync<T>(sql, param, GetTransaction(TransactionMode.ReadOnly));
    }

    public Task ExecuteAsync(string sql, object? param = null)
    {
        return GetConnection()
            .ExecuteAsync(sql, param, GetTransaction(TransactionMode.ReadWrite));
    }

    public Task<T> ExecuteWithOutputAsync<T>(string sql, object? param = null)
    {
        return GetConnection()
            .QuerySingleAsync<T>(sql, param, GetTransaction(TransactionMode.ReadWrite));
    }
}