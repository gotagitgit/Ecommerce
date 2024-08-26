namespace Inventory.Domain.Models;

public sealed record Order(Guid Id, Guid QuoteId, Guid Sku, int Quantity)
{
}
