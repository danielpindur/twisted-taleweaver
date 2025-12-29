using Dapper;
using Npgsql;
using TwistedTaleweaver.Core.Database;
using TwistedTaleweaver.Core.Database.Enums;

namespace TwistedTaleweaver.DataAccess.Common;

public abstract class DatabaseAccessor(IUnitOfWorkProvider unitOfWorkProvider) : IDatabaseAccessor
{
    private IUnitOfWork? _operationUnitOfWork;

    private IUnitOfWork GetUnitOfWork()
    {
        if (_operationUnitOfWork != null)
        {
            return _operationUnitOfWork;
        }
        
        var activeUnitOfWork = unitOfWorkProvider.GetActiveUnitOfWork();

        if (activeUnitOfWork != null)
        {
            // We are in a scope -> use existing unit of work
            return activeUnitOfWork;
        }
        
        // We are outside scope -> use unit of work specific for this operation
        _operationUnitOfWork = unitOfWorkProvider.CreateOperationUnitOfWork();
        return _operationUnitOfWork;
    }

    private async Task CloseOperationUnitOfWork()
    {
        if (_operationUnitOfWork != null)
        {
            await _operationUnitOfWork.DisposeAsync();
            _operationUnitOfWork = null;
        }
    }
    
    private NpgsqlConnection GetConnection()
    {
        var unitOfWork = GetUnitOfWork();
        
        return unitOfWork.Connection.Value as NpgsqlConnection
               ?? throw new InvalidOperationException("The database connection is not a valid NpgsqlConnection.");
    }

    private NpgsqlTransaction? GetTransaction(TransactionMode mode)
    {
        var unitOfWork = GetUnitOfWork();
        
        var transaction = unitOfWork.GetTransaction(mode);

        if (transaction == null)
        {
            return null;
        }

        return transaction as NpgsqlTransaction
            ?? throw new InvalidOperationException("The database transaction is not a valid NpgsqlTransaction.");
    }

    public async Task<T?> QuerySingleOrDefaultAsync<T>(string sql, object? param = null)
    {
        var result = await GetConnection()
            .QuerySingleOrDefaultAsync<T>(sql, param, GetTransaction(TransactionMode.ReadOnly));

        await CloseOperationUnitOfWork();

        return result;
    }

    public async Task<T> QuerySingleAsync<T>(string sql, object? param = null)
    {
        var result = await GetConnection()
            .QuerySingleAsync<T>(sql, param, GetTransaction(TransactionMode.ReadOnly));
        
        await CloseOperationUnitOfWork();
        
        return result;
    }

    public async Task<IEnumerable<T>> QueryAsync<T>(string sql, object? param = null)
    {
        var result = await GetConnection()
            .QueryAsync<T>(sql, param, GetTransaction(TransactionMode.ReadOnly));
        
        await CloseOperationUnitOfWork();
        
        return result;
    }

    public async Task ExecuteAsync(string sql, object? param = null)
    {
        await GetConnection()
            .ExecuteAsync(sql, param, GetTransaction(TransactionMode.ReadWrite));

        await CloseOperationUnitOfWork();
    }

    public async Task<T> ExecuteWithOutputAsync<T>(string sql, object? param = null)
    {
        var result = await GetConnection()
            .QuerySingleAsync<T>(sql, param, GetTransaction(TransactionMode.ReadWrite));
        
        await CloseOperationUnitOfWork();
        
        return result;
    }
}