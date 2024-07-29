namespace Inventory.Domain.Models;

public sealed record Product(Guid SKU, string Name, int Quantity)
{
    public static Product Empty => new(Guid.Empty, "", 0);

    public bool IsEmpty => SKU == Guid.Empty;
}