using Inventory.Application.Repositories;
using Inventory.Domain.Models;
using MediatR;

namespace Inventory.Application.Features.Products.Commands.AddProduct;

internal sealed class AddProductCommandHandler : IRequestHandler<AddProductCommand, Guid>
{
    private readonly IProductRepository _productRepository;

    public AddProductCommandHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<Guid> Handle(AddProductCommand request, CancellationToken cancellationToken)
    {
        var productId = Guid.NewGuid();

        var product = new Product(productId, request.Name, 0);

        await _productRepository.InsertAsync(product);

        return productId;
    }
}
