using Npgsql;

namespace TwistedTaleweaver.DataAccess.Common.Extensions;

public static class UnitOfWorkExtensions
{
    public static async Task ExecuteInTransactionAsync(
        this IUnitOfWork unitOfWork, 
        Func<NpgsqlTransaction, Task> operation)
    {
        var transaction = unitOfWork.BeginTransaction();
        try
        {
            await operation(transaction);
            await unitOfWork.CommitAsync();
        }
        catch
        {
            await unitOfWork.RollbackAsync();
            throw;
        }
    }
}