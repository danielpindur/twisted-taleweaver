using System.Collections.Concurrent;
using TwistedTaleweaver.Core.Database.Enums;

namespace TwistedTaleweaver.Core.Database;

public interface IUnitOfWorkProvider : IDisposable, IAsyncDisposable
{
    IUnitOfWork CreateUnitOfWork(UnitOfWorkScopeOption scopeOption);

    IUnitOfWork CreateOperationUnitOfWork();
    
    IUnitOfWork? GetActiveUnitOfWork();

    Task CloseUnitOfWorkAsync();

    Task CommitUnitOfWorkAsync();

    Task RollbackUnitOfWorkAsync();
}

internal class UnitOfWorkProvider(IDbConnectionFactory connectionFactory) : IUnitOfWorkProvider
{
    private static readonly AsyncLocal<Stack<UnitOfWorkStackEntry>> _unitsOfWork = new();
    private static Stack<UnitOfWorkStackEntry> UnitsOfWork => _unitsOfWork.Value ??= new Stack<UnitOfWorkStackEntry>();

    /// <summary>
    /// Called by UnitOfWorkScope on new scope creation
    /// </summary>
    public IUnitOfWork CreateUnitOfWork(UnitOfWorkScopeOption scopeOption)
    {
        var stack = UnitsOfWork;
        
        // We haven't connected to the database - create new one
        if (stack.Count == 0)
        {
            // Create new unit of work with a new connection
            var unitOfWorkEntry = new UnitOfWorkStackEntry(connectionFactory, scopeOption);
            stack.Push(unitOfWorkEntry);

            return unitOfWorkEntry.UnitOfWork;
        }

        var activeUnitOfWorkEntry = stack.Peek();
            
        // We have done either some writes / reads outside of transaction and now want a transaction
        if (activeUnitOfWorkEntry.UnitOfWork.ScopeOption == UnitOfWorkScopeOption.Suppressed &&
            scopeOption == UnitOfWorkScopeOption.Required)
        {
            // Create new unit of work with a new connection
            var unitOfWorkEntry = new UnitOfWorkStackEntry(connectionFactory, scopeOption);
            stack.Push(unitOfWorkEntry);

            return unitOfWorkEntry.UnitOfWork;
        }

        // We are in a transaction and need to write to the database irrespective of the transaction
        if (activeUnitOfWorkEntry.UnitOfWork.ScopeOption == UnitOfWorkScopeOption.Required &&
            scopeOption == UnitOfWorkScopeOption.Suppressed)
        {
            // Create new unit of work with a new connection
            var unitOfWorkEntry = new UnitOfWorkStackEntry(connectionFactory, scopeOption);
            stack.Push(unitOfWorkEntry);

            return unitOfWorkEntry.UnitOfWork;
        }

        // We are in transaction and inner transaction scope is being created
        if (activeUnitOfWorkEntry.UnitOfWork.ScopeOption == UnitOfWorkScopeOption.Required &&
            scopeOption == UnitOfWorkScopeOption.Required)
        {
            activeUnitOfWorkEntry.Connect();
            return activeUnitOfWorkEntry.UnitOfWork;
        }

        // We are outside of a transaction and new suppressed scope is being created
        if (activeUnitOfWorkEntry.UnitOfWork.ScopeOption == UnitOfWorkScopeOption.Suppressed &&
            scopeOption == UnitOfWorkScopeOption.Suppressed)
        {
            activeUnitOfWorkEntry.Connect();
            return activeUnitOfWorkEntry.UnitOfWork;
        }

        throw new InvalidOperationException("Unhandled unit of work scope option combination.");
    }

    /// <summary>
    /// Called by DatabaseAccessor to create a new unit of work for a single operation
    /// </summary>
    public IUnitOfWork CreateOperationUnitOfWork()
    {
        return new UnitOfWork(connectionFactory, UnitOfWorkScopeOption.Suppressed);
    }

    /// <summary>
    /// Called by DatabaseAccessor to resolve the unit of work to use
    /// </summary>
    public IUnitOfWork? GetActiveUnitOfWork()
    {
        // If we have one, just return it
        return UnitsOfWork.TryPeek(out var unitOfWork) ? unitOfWork.UnitOfWork : null;
    }

    public async Task CloseUnitOfWorkAsync()
    {
        var stack = UnitsOfWork;
        var activeUnitOfWorkEntry = stack.Peek();
        
        activeUnitOfWorkEntry.Disconnect();

        if (activeUnitOfWorkEntry.HasNoConnections)
        {
            stack.Pop();
            
            await activeUnitOfWorkEntry.UnitOfWork.DisposeAsync();
        }
    }

    public async Task CommitUnitOfWorkAsync()
    {
        var stack = UnitsOfWork;
        var activeUnitOfWorkEntry = stack.Peek();

        if (activeUnitOfWorkEntry.IsRootConnection &&
            activeUnitOfWorkEntry.UnitOfWork.ScopeOption == UnitOfWorkScopeOption.Required)
        {
            await activeUnitOfWorkEntry.UnitOfWork.CommitAsync();
        }
    }

    public async Task RollbackUnitOfWorkAsync()
    {
        var stack = UnitsOfWork;
        var activeUnitOfWorkEntry = stack.Peek();
        
        activeUnitOfWorkEntry.Disconnect();

        if (activeUnitOfWorkEntry.HasNoConnections &&
            activeUnitOfWorkEntry.UnitOfWork.ScopeOption == UnitOfWorkScopeOption.Required)
        {
            await activeUnitOfWorkEntry.UnitOfWork.RollbackAsync();
        }
    }

    public async ValueTask DisposeAsync()
    {
        var stack = UnitsOfWork;
        while (stack.TryPop(out var unitOfWork))
        {
            await unitOfWork.UnitOfWork.DisposeAsync();
        }
    }

    private class UnitOfWorkStackEntry(
        IDbConnectionFactory connectionFactory,
        UnitOfWorkScopeOption scopeOption)
    {
        private int _connectionsCount = 1;
        public IUnitOfWork UnitOfWork { get; } = new UnitOfWork(connectionFactory, scopeOption);
        
        public void Connect()
        {
            Interlocked.Increment(ref _connectionsCount);
        }

        public void Disconnect()
        {
            Interlocked.Decrement(ref _connectionsCount);
        }

        public bool HasNoConnections => Volatile.Read(ref _connectionsCount) == 0;
        public bool IsRootConnection => Volatile.Read(ref _connectionsCount) == 1;
    }

    public void Dispose()
    {
        Task.Run(DisposeAsync);
    }
}