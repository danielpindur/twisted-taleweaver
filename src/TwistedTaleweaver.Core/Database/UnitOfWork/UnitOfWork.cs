using System.Data.Common;
using TwistedTaleweaver.Core.Database.UnitOfWork.Enums;

namespace TwistedTaleweaver.Core.Database.UnitOfWork;

public interface IUnitOfWork : IAsyncDisposable
{
    Task CommitAsync();

    Task RollbackAsync();
    
    UnitOfWorkScopeOption ScopeOption { get; }
    
    DbConnection Connection { get; }

    DbTransaction? GetTransaction(ConnectionMode connectionMode);
}

internal class UnitOfWork : IUnitOfWork
{
    private DbTransaction? _transaction;
    
    public UnitOfWork(
        IDbConnectionFactory connectionFactory,
        ConnectionMode connectionMode,
        UnitOfWorkScopeOption scopeOption)
    {
        ScopeOption = scopeOption;
        _connectionMode = connectionMode;
        
        Connection = connectionFactory.CreateConnection();
        Connection.Open();
    }

    public DbConnection Connection { get; }
    
    public DbTransaction? GetTransaction(ConnectionMode connectionMode)
    {
        // We are suppressing the transaction itself
        if (ScopeOption == UnitOfWorkScopeOption.Suppress)
        {
            return null;
        }

        // ReadOnly connection requested and we are already in ReadOnly mode
        if (_connectionMode == ConnectionMode.ReadOnly && connectionMode == ConnectionMode.ReadOnly)
        {
            return null;
        }

        // We need a transaction, either get active one or create one
        _connectionMode = ConnectionMode.ReadWrite;
        return _transaction ??= Connection.BeginTransaction();
    }

    public UnitOfWorkScopeOption ScopeOption { get; }

    private ConnectionMode _connectionMode;

    public async Task CommitAsync()
    {
        if (_transaction is null)
        {
            throw new InvalidOperationException("Cannot commit non-existing transaction");
        }
        
        await _transaction.CommitAsync();
        await _transaction.DisposeAsync();
        _transaction = null;
    }

    public async Task RollbackAsync()
    {
        if (_transaction is null)
        {
            throw new InvalidOperationException("Cannot rollback non-existing transaction");
        }
        
        await _transaction.RollbackAsync();
        await _transaction.DisposeAsync();
        _transaction = null;
    }
    
    public async ValueTask DisposeAsync()
    {
        if (_transaction is not null)
        {
            throw new InvalidOperationException("Cannot dispose unit of work with active transaction");
        }

        await Connection.DisposeAsync();
    }
}