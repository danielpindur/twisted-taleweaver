using TwistedTaleweaver.Core.Database.Enums;
using TwistedTaleweaver.Core.DependencyInjection;

namespace TwistedTaleweaver.Core.Database;

public class UnitOfWorkScope : IAsyncDisposable
{
    private readonly IUnitOfWorkProvider _unitOfWorkProvider;
    private bool _isCommitted;

    public UnitOfWorkScope(UnitOfWorkScopeOption scopeOption = UnitOfWorkScopeOption.Required)
    {
        _unitOfWorkProvider = ServiceLocator.GetService<IUnitOfWorkProvider>();

        _unitOfWorkProvider.CreateUnitOfWork(scopeOption);
    }

    public async ValueTask DisposeAsync()
    {
        if (!_isCommitted)
        {
            await _unitOfWorkProvider.RollbackUnitOfWorkAsync();
        }

        await _unitOfWorkProvider.CloseUnitOfWorkAsync();
    }

    public async Task CompleteAsync()
    {
        if (_isCommitted)
        {
            throw new InvalidOperationException("Unit of work scope has already been completed.");
        }

        await _unitOfWorkProvider.CommitUnitOfWorkAsync();
        _isCommitted = true;
    }
}