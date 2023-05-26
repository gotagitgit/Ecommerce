namespace Inventory.Domain.Models;

public sealed record Product(Guid SKU, string Name, int Quantity);