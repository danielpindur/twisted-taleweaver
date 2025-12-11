using TwistedTaleweaver.Core.Database.Enums;

namespace TwistedTaleweaver.Core.Database;

internal interface IUnitOfWorkProvider
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

    public IUnitOfWork CreateUnitOfWork(UnitOfWorkScopeOption scopeOption)
    {
        // We haven't connected to the database - create new one
        if (_unitsOfWork.Count == 0)
        {
            _unitsOfWork.Push(new UnitOfWorkStackEntry(connectionFactory, ConnectionMode.ReadWrite, scopeOption));
            return GetActiveUnitOfWork();
        }

        if (GetActiveUnitOfWork().ScopeOption == UnitOfWorkScopeOption.Suppress &&
            scopeOption == UnitOfWorkScopeOption.Required)
        {
            // In theory, we might just need to create a new connection with a transaction,
            // but seems really weird to do it in this order, so let's just throw for now
            throw new InvalidOperationException("Cannot create a transactional unit of work within a suppressed scope.");
        }

        if (GetActiveUnitOfWork().ScopeOption == UnitOfWorkScopeOption.Required &&
            scopeOption == UnitOfWorkScopeOption.Suppress)
        {
            // We need to write to the database irrespective of the current transaction
            // -> we need a new unit of work
            _unitsOfWork.Push(new UnitOfWorkStackEntry(connectionFactory, ConnectionMode.ReadWrite, scopeOption));
            return GetActiveUnitOfWork();
        }

        if (GetActiveUnitOfWork().ScopeOption == UnitOfWorkScopeOption.Required &&
            scopeOption == UnitOfWorkScopeOption.Required)
        {
            _unitsOfWork.Peek().Connect();
            return GetActiveUnitOfWork();
        }
        
        if (GetActiveUnitOfWork().ScopeOption == UnitOfWorkScopeOption.Suppress &&
            scopeOption == UnitOfWorkScopeOption.Suppress)
        {
            _unitsOfWork.Peek().Connect();
            return GetActiveUnitOfWork();
        }
        
        throw new InvalidOperationException("Unhandled unit of work scope option combination.");
    }

    public IUnitOfWork GetActiveUnitOfWork()
    {
        if (_unitsOfWork.TryPop(out var unitOfWork))
        {
            return unitOfWork.UnitOfWork;
        }
        
        var unitOfWorkEntry = new UnitOfWorkStackEntry(connectionFactory, ConnectionMode.ReadOnly, UnitOfWorkScopeOption.Required);
        _unitsOfWork.Push(unitOfWorkEntry);
        
        return unitOfWorkEntry.UnitOfWork;
    }

    public async Task CloseUnitOfWorkAsync()
    {
        var activeEntry = _unitsOfWork.Peek();
        activeEntry.Disconnect();
        
        if (activeEntry.HasNoConnections)
        {
            await GetActiveUnitOfWork().DisposeAsync();
        }
    }

    public async Task CommitUnitOfWorkAsync()
    {
        var activeEntry = _unitsOfWork.Peek();
        activeEntry.Disconnect();
        
        if (activeEntry.HasNoConnections && 
            activeEntry.UnitOfWork.ScopeOption != UnitOfWorkScopeOption.Suppress)
        {
            await GetActiveUnitOfWork().CommitAsync();
        }
    }
    
    public async Task RollbackUnitOfWorkAsync()
    {
        var activeEntry = _unitsOfWork.Peek();
        activeEntry.Disconnect();
        
        if (activeEntry.HasNoConnections && 
            activeEntry.UnitOfWork.ScopeOption != UnitOfWorkScopeOption.Suppress)
        {
            await GetActiveUnitOfWork().RollbackAsync();
        }
    }

    private class UnitOfWorkStackEntry(
        IDbConnectionFactory connectionFactory,
        ConnectionMode connectionMode,
        UnitOfWorkScopeOption scopeOption)
    {
        private int _connectionsCount = 1;
        public IUnitOfWork UnitOfWork { get; } = new UnitOfWork(connectionFactory, connectionMode, scopeOption);

        public void Connect()
        {
            _connectionsCount++;
        }
        
        public void Disconnect()
        {
            _connectionsCount--;
        }
        
        public bool HasNoConnections => _connectionsCount == 0;
    }
}