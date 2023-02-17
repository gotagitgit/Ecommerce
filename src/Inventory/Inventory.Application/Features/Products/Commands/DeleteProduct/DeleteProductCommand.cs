using MediatR;

namespace Inventory.Application.Features.Products.Commands.DeleteProduct;

public sealed class DeleteProductCommand : IRequest
{
    public Guid Sku { get; set; }
}
