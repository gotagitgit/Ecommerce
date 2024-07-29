using Inventory.Domain.Models;
using Inventory.SQLiteInfrastructure.Products.Dtos;

namespace Inventory.SQLiteInfrastructure.Products.Factories;

internal class ProductFactory
{
    public static Product ToModel(ProductDto product)
    {
        return new Product(
            product.Id,
            product.Name,
            product.Quantity);
    }

    public static ProductDto ToDto(Product product)
    {
        return new ProductDto
        {
            Id = product.SKU,
            Name = product.Name,
            Quantity = product.Quantity
        };
    }
}
