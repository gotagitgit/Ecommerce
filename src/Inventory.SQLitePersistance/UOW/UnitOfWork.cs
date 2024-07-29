using Infrastructure.Common;
using Inventory.SQLiteInfrastructure.Persistance;

namespace Inventory.SQLiteInfrastructure.UOW;

public class UnitOfWork(IDatabaseContext<SQLiteDbContext> dbContext) : IUnitOfWork<SQLiteDbContext>
{
    private readonly SQLiteDbContext _dbContext = dbContext.DbContext;

    public async Task ExecuteReadOnlyTransactionAsync(Func<IDatabaseContext<SQLiteDbContext>, Task> callback)
    {
        using var transaction = await _dbContext.Database.BeginTransactionAsync();

        try
        {
            await callback(_dbContext);
        }
        catch (Exception)
        {
            await transaction.RollbackAsync();
            throw;
        }
        finally
        {
            await transaction.CommitAsync();

            await transaction.DisposeAsync();
        }
    }  

    public async Task<T> ExecuteReadWriteTransactionAsync<T>(Func<IDatabaseContext<SQLiteDbContext>, Task<T>> callback)
    {
        using var transaction = await _dbContext.Database.BeginTransactionAsync();

        try
        {
            return await callback(_dbContext);
        }
        catch (Exception)
        {
            await transaction.RollbackAsync();
            throw;
        }
        finally
        {
            await transaction.CommitAsync();

            await transaction.DisposeAsync();
        }
    }   
}
