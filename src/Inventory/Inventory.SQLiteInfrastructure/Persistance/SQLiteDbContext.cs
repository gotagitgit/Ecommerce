using Infrastructure.Common;
using Inventory.SQLiteInfrastructure.Dtos;
using Inventory.SQLiteInfrastructure.Products.Dtos;
using Microsoft.EntityFrameworkCore;

namespace Inventory.SQLiteInfrastructure.Persistance;

public class SQLiteDbContext(DbContextOptions<SQLiteDbContext> options) : DbContext(options), IDatabaseContext
{
    public DbSet<ProductDto> Products { get; set; }

    public DbSet<OrderDto> Orders { get; set; }

    public DbSet<QuoteDto> Quotes { get; set; }

    public async Task BeginTransactionAsync() => _ = await Database.BeginTransactionAsync();            

    public async Task CommitTransactionAsync()
    {
        var transaction = Database.CurrentTransaction;

        if (transaction == null)
            return;

        await transaction.CommitAsync();
    }

    public async Task RollbackTransactionAsync()
    {
        var transaction = Database.CurrentTransaction;        

        if (transaction == null)
            return;

        await transaction.RollbackAsync();
    }

    public bool Disposed { get; private set; }

    public override void Dispose()
    {
        if (!Disposed)
        {
            Disposed = true;
            base.Dispose();
        }
    }

    public override async ValueTask DisposeAsync()
    {
        Dispose();
        await Task.CompletedTask;
    }
}

