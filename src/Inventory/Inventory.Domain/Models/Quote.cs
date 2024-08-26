namespace Inventory.Domain.Models;

public sealed record Quote(Guid Id, Guid CustomerId, DateTime DateCreated, IReadOnlyList<Order> Orders)
{
    public static Quote Empty => new(Guid.Empty, Guid.Empty, (default), []);
}
