namespace Inventory.Domain.Models;

public sealed class Product
{
    public Product(Guid sKU, string name, int quantity)
    {
        SKU=sKU;
        Name=name;
        Quantity=quantity;
    }

    public Guid SKU { get; }

    public string Name { get; } 

    public int Quantity { get; }
}
