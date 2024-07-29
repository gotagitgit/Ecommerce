using Inventory.SQLiteInfrastructure.Products.Dtos;
using Microsoft.EntityFrameworkCore;

namespace Inventory.SQLiteInfrastructure.Persistance;

public interface ISQLiteDbContext
{
    DbSet<ProductDto> Products { get; set; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}