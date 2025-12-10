using Npgsql;

namespace TwistedTaleweaver.DataAccess.Common.Transactions.Extensions;

internal static class UnitOfWorkExtensions
{
    extension(IUnitOfWork unitOfWork)
    {
        public async Task ExecuteInTransactionAsync(Func<NpgsqlTransaction, Task> operation)
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
        
        public async Task<T> ExecuteInTransactionAsync<T>(Func<NpgsqlTransaction, Task<T>> operation)
        {
            var transaction = unitOfWork.BeginTransaction();
            try
            {
                var result = await operation(transaction);
                await unitOfWork.CommitAsync();
                
                return result;
            }
            catch
            {
                await unitOfWork.RollbackAsync();
                throw;
            }
        }
    }
}