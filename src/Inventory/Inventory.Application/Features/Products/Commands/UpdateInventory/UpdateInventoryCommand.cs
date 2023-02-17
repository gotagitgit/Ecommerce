using MediatR;

namespace Inventory.Application.Features.Products.Commands.UpdateInventory;

public sealed class UpdateInventoryCommand : IRequest
{
    public Guid Sku { get; set; }
    public int Quantity { get; set; }
}
