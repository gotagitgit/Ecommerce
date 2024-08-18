using Inventory.SQLiteInfrastructure.Products.Dtos;

namespace Inventory.SQLiteInfrastructure.Persistance.Seeders;

internal class ProductsSeeder : IDBContextSeeder
{
    public void Seed(SQLiteDbContext context)
    {
        if (!context.Products.Any())
        {
            var products = new List<ProductDto>
            {
                new() { Id = Guid.NewGuid(), Name = "Product 1", Quantity = 2 },
                new() { Id = Guid.NewGuid(), Name = "Product 2", Quantity = 4 },
                new() { Id = Guid.NewGuid(), Name = "Product 3", Quantity = 6 },
            };

            context.Products.AddRange(products);

            context.SaveChanges();
        }
    }
}
