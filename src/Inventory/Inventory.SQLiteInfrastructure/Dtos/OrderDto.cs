namespace Inventory.SQLiteInfrastructure.Dtos;

public sealed class OrderDto
{
    public Guid Id { get; set; }

    public Guid QuoteId { get; set; }

    public Guid Sku { get; set; }

    public int Quantity { get; set; }
}
