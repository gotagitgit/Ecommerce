using Infrastructure.Common;
using Inventory.SQLiteInfrastructure.Dtos;
using Inventory.SQLiteInfrastructure.Products.Dtos;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Inventory.SQLiteInfrastructure.Persistance;

public class SQLiteDbContext(DbContextOptions<SQLiteDbContext> options) : DbContext(options), IDatabaseContext
{
    public DbSet<ProductDto> Products { get; set; }

    public DbSet<OrderDto> Orders { get; set; }

    public DbSet<QuoteDto> Quotes { get; set; }

    public async Task BeginTransactionAsync()
    {
        var connection = Database.GetDbConnection();

        if (connection.State != ConnectionState.Open)
            await connection.OpenAsync();

        var transaction = await connection.BeginTransactionAsync();

        await Database.UseTransactionAsync(transaction);
    }

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
}

