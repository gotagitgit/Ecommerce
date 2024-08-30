
namespace Infrastructure.Common;

public interface IDatabaseContext
{
    Task BeginTransactionAsync();

    Task CommitTransactionAsync();

    Task RollbackTransactionAsync();
}