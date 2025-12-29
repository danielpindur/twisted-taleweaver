using System.Data.Common;
using TwistedTaleweaver.Core.Database.Enums;

namespace TwistedTaleweaver.Core.Database;

public interface IUnitOfWork : IAsyncDisposable
{
    Task CommitAsync();

    Task RollbackAsync();

    UnitOfWorkScopeOption ScopeOption { get; }

    Lazy<DbConnection> Connection { get; }

    DbTransaction? GetTransaction(TransactionMode mode);
}

internal class UnitOfWork : IUnitOfWork
{
    private DbTransaction? _transaction;

    public UnitOfWork(
        IDbConnectionFactory connectionFactory,
        UnitOfWorkScopeOption scopeOption)
    {
        ScopeOption = scopeOption;
        Connection = new  Lazy<DbConnection>(() => OpenConnection(connectionFactory));
    }

    public Lazy<DbConnection> Connection { get; }

    private DbConnection OpenConnection(IDbConnectionFactory connectionFactory)
    {
        var connection = connectionFactory.CreateConnection();
        connection.Open();

        return connection;
    }
    
    public DbTransaction? GetTransaction(TransactionMode mode)
    {
        // We are suppressing the transaction itself
        if (ScopeOption == UnitOfWorkScopeOption.Suppressed)
        {
            return null;
        }

        if (mode == TransactionMode.ReadOnly)
        {
            // If we are only reading and we don't have a transaction, no need to create one
            return _transaction;
        }

        // We need a transaction, either get active one or create one
        return _transaction ??= Connection.Value.BeginTransaction();
    }

    public UnitOfWorkScopeOption ScopeOption { get; }

    public async Task CommitAsync()
    {
        if (ScopeOption == UnitOfWorkScopeOption.Suppressed)
        {
            throw new InvalidOperationException("Cannot commit transaction in suppressed scope");
        }

        if (_transaction is null)
        {
            // We are in a scope with transaction, but no write operations were performed
            return;
        }

        await _transaction.CommitAsync();
        await _transaction.DisposeAsync();
        _transaction = null;
    }

    public async Task RollbackAsync()
    {
        if (ScopeOption == UnitOfWorkScopeOption.Suppressed)
        {
            throw new InvalidOperationException("Cannot rollback transaction in suppressed scope");
        }

        if (_transaction is null)
        {
            // We are in a scope with transaction, but no write operations were performed
            return;
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

        await Connection.Value.DisposeAsync();
    }
}