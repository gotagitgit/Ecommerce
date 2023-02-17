using Inventory.Infrastructure.Dtos;
using MongoDB.Driver;

namespace Inventory.Infrastructure.Persistance;

internal sealed class ProductSeedData
{
    public static void SeedData(IMongoCollection<ProductDto> productCollection)
    {
        bool existProduct = productCollection.Find(p => true).Any();
        
        if (!existProduct)        
            productCollection.InsertManyAsync(GetProducts());        
    }

    private static List<ProductDto> GetProducts()
    {
        return new List<ProductDto>
        {
            new ProductDto
            {
                Id = Guid.NewGuid(),
                Name = "iPhone",
                Quantity = 10
            },
            new ProductDto
            {
                Id = Guid.NewGuid(),
                Name = "Samsung",
                Quantity = 20
            },
            new ProductDto 
            {
                Id = Guid.NewGuid(),
                Name = "Nokia",
                Quantity = 30
            }
        };
    }
}
