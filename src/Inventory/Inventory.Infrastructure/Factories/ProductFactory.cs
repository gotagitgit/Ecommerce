using Inventory.Domain.Models;
using Inventory.Infrastructure.Dtos;

namespace Inventory.Infrastructure.Factories;

internal sealed class ProductFactory 
{
    public static Product ToModel(ProductDto product)
    {
        //if (!Guid.TryParse(product.Sku, out var sku))
        //    throw new ArgumentException($"{product.Sku} is not valid SKU");

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
