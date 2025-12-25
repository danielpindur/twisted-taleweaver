using TwistedTaleweaver.Core.Database.Enums;

namespace TwistedTaleweaver.Core.Database;

public interface IUnitOfWorkProvider : IAsyncDisposable
{
    IUnitOfWork CreateUnitOfWork(UnitOfWorkScopeOption scopeOption);

    IUnitOfWork GetActiveUnitOfWork();

    Task CloseUnitOfWorkAsync();

    Task CommitUnitOfWorkAsync();

    Task RollbackUnitOfWorkAsync();
}

internal class UnitOfWorkProvider(IDbConnectionFactory connectionFactory) : IUnitOfWorkProvider
{
    private readonly Stack<UnitOfWorkStackEntry> _unitsOfWork = new();

    /// <summary>
    /// Called by UnitOfWorkScope on new scope creation
    /// </summary>
    public IUnitOfWork CreateUnitOfWork(UnitOfWorkScopeOption scopeOption)
    {
        // We haven't connected to the database - create new one
        if (_unitsOfWork.Count == 0)
        {
            // Create new unit of work with a new connection
            var unitOfWorkEntry = new UnitOfWorkStackEntry(connectionFactory, scopeOption);
            _unitsOfWork.Push(unitOfWorkEntry);

            return unitOfWorkEntry.UnitOfWork;
        }

        // We have done either some writes / reads outside of transaction and now want a transaction
        if (_unitsOfWork.Peek().UnitOfWork.ScopeOption == UnitOfWorkScopeOption.Suppressed &&
            scopeOption == UnitOfWorkScopeOption.Required)
        {
            // Create new unit of work with a new connection
            var unitOfWorkEntry = new UnitOfWorkStackEntry(connectionFactory, scopeOption);
            _unitsOfWork.Push(unitOfWorkEntry);

            return unitOfWorkEntry.UnitOfWork;
        }

        // We are in a transaction and need to write to the database irrespective of the transaction
        if (_unitsOfWork.Peek().UnitOfWork.ScopeOption == UnitOfWorkScopeOption.Required &&
            scopeOption == UnitOfWorkScopeOption.Suppressed)
        {
            // Create new unit of work with a new connection
            var unitOfWorkEntry = new UnitOfWorkStackEntry(connectionFactory, scopeOption);
            _unitsOfWork.Push(unitOfWorkEntry);

            return unitOfWorkEntry.UnitOfWork;
        }

        // We are in transaction and inner transaction scope is being created
        if (_unitsOfWork.Peek().UnitOfWork.ScopeOption == UnitOfWorkScopeOption.Required &&
            scopeOption == UnitOfWorkScopeOption.Required)
        {
            var unitOfWorkEntry = _unitsOfWork.Peek();
            unitOfWorkEntry.Connect();

            return unitOfWorkEntry.UnitOfWork;
        }

        // We are outside of a transaction and new suppressed scope is being created
        if (_unitsOfWork.Peek().UnitOfWork.ScopeOption == UnitOfWorkScopeOption.Suppressed &&
            scopeOption == UnitOfWorkScopeOption.Suppressed)
        {
            var unitOfWorkEntry = _unitsOfWork.Peek();
            unitOfWorkEntry.Connect();

            return unitOfWorkEntry.UnitOfWork;
        }

        throw new InvalidOperationException("Unhandled unit of work scope option combination.");
    }

    /// <summary>
    /// Called by DatabaseAccessor to resolve the unit of work to use
    /// </summary>
    public IUnitOfWork GetActiveUnitOfWork()
    {
        // If we have one, just return it
        if (_unitsOfWork.TryPeek(out var unitOfWork))
        {
            return unitOfWork.UnitOfWork;
        }

        // If we have none -> we are not in a UnitOfWorkScope (as that creates the unit of work)
        // -> get one with transaction suppression
        return CreateUnitOfWork(UnitOfWorkScopeOption.Suppressed);
    }

    public async Task CloseUnitOfWorkAsync()
    {
        var activeEntry = _unitsOfWork.Peek();
        activeEntry.Disconnect();

        if (activeEntry.HasNoConnections)
        {
            _unitsOfWork.Pop();
            await activeEntry.UnitOfWork.DisposeAsync();
        }
    }

    public async Task CommitUnitOfWorkAsync()
    {
        var activeEntry = _unitsOfWork.Peek();

        if (activeEntry.IsRootConnection &&
            activeEntry.UnitOfWork.ScopeOption == UnitOfWorkScopeOption.Required)
        {
            await activeEntry.UnitOfWork.CommitAsync();
        }
    }

    public async Task RollbackUnitOfWorkAsync()
    {
        var activeEntry = _unitsOfWork.Peek();
        activeEntry.Disconnect();

        if (activeEntry.HasNoConnections &&
            activeEntry.UnitOfWork.ScopeOption == UnitOfWorkScopeOption.Required)
        {
            await activeEntry.UnitOfWork.RollbackAsync();
        }
    }

    public async ValueTask DisposeAsync()
    {
        while (_unitsOfWork.TryPop(out var unitOfWork))
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
            _connectionsCount++;
        }

        public void Disconnect()
        {
            _connectionsCount--;
        }

        public bool HasNoConnections => _connectionsCount == 0;

        public bool IsRootConnection => _connectionsCount == 1;
    }
}