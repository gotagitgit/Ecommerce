using MediatR;

namespace Inventory.Application.Features.Products.Commands.AddProduct;

public sealed class AddProductCommand : IRequest<Guid>
{
    public string Name { get; set; }
}
