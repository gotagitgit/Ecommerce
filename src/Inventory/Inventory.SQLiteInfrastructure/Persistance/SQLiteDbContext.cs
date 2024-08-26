using Infrastructure.Common;
using Inventory.SQLiteInfrastructure.Dtos;
using Inventory.SQLiteInfrastructure.Products.Dtos;
using Microsoft.EntityFrameworkCore;

namespace Inventory.SQLiteInfrastructure.Persistance;

public class SQLiteDbContext(DbContextOptions<SQLiteDbContext> options) : DbContext(options), IDatabaseContext<SQLiteDbContext>
{
    public DbSet<ProductDto> Products { get; set; }

    public DbSet<OrderDto> Orders { get; set; }

    public DbSet<QuoteDto> Quotes { get; set; }

    public SQLiteDbContext DbContext => this;

    //    protected override void OnConfiguring(DbContextOptionsBuilder options)
    //    => options.UseSqlite("DataSource=Inventories.db");  
}

