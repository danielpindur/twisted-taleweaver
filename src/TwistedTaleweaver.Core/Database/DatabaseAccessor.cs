using System.Data;
using Dapper;
using TwistedTaleweaver.Core.Database.Enums;

namespace TwistedTaleweaver.Core.Database;

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
    
    private IDbConnection GetConnection()
    {
        var unitOfWork = GetUnitOfWork();
        return unitOfWork.Connection.Value;
    }

    private IDbTransaction? GetTransaction(TransactionMode mode)
    {
        var unitOfWork = GetUnitOfWork();
        return unitOfWork.GetTransaction(mode);
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