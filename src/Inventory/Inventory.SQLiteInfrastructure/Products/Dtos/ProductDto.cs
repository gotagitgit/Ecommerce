namespace Inventory.SQLiteInfrastructure.Products.Dtos;

public class ProductDto
{
    public Guid Id { get; set; }

    public string Name { get; set; } = String.Empty;

    public int Quantity { get; set; }
}
