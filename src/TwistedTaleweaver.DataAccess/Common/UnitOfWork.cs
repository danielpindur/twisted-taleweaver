using Npgsql;

namespace TwistedTaleweaver.DataAccess.Common;

public interface IUnitOfWork : IAsyncDisposable
{
    NpgsqlTransaction BeginTransaction();
 
    Task CommitAsync();

    Task RollbackAsync();
}

internal class UnitOfWork : IUnitOfWork
{
    private readonly NpgsqlConnection _connection;
    private NpgsqlTransaction? _transaction;

    public UnitOfWork(IDbConnectionFactory connectionFactory)
    {
        _connection = connectionFactory.CreateConnection();
        _connection.Open();
    }

    public NpgsqlTransaction BeginTransaction()
    {
        if (_transaction is not null)
        {
            throw new InvalidOperationException("Transaction is already open");
        }
        
        _transaction = _connection.BeginTransaction();
        return _transaction;
    }

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
            await _transaction.DisposeAsync();
        }

        await _connection.DisposeAsync();
    }
}