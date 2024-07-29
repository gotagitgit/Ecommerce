namespace Infrastructure.Common;

public interface IUnitOfWork<TContext>
{
    Task ExecuteReadOnlyTransactionAsync(Func<IDatabaseContext<TContext>, Task> callback);

    Task<T> ExecuteReadWriteTransactionAsync<T>(Func<IDatabaseContext<TContext>, Task<T>> callback);
}