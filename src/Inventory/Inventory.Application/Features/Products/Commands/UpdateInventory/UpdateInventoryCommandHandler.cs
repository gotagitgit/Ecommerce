using Inventory.Application.Repositories;
using MediatR;

namespace Inventory.Application.Features.Products.Commands.UpdateInventory;

internal sealed class UpdateInventoryCommandHandler : IRequestHandler<UpdateInventoryCommand>
{
    private readonly IProductRepository _productRepository;

    public UpdateInventoryCommandHandler(IProductRepository productRepository)
    {
        _productRepository=productRepository;
    }

    public async Task<Unit> Handle(UpdateInventoryCommand request, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetAsync(request.Sku);

        if (product == null)
            throw new ArgumentException($"Product {request.Sku} does not exist");

        var updatedProduct = product with { Quantity = request.Quantity };

        await _productRepository.UpdateAsync(updatedProduct);

        return Unit.Value;
    }
}
