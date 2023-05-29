using Inventory.Application.Features.Products.Commands.AddProduct;

namespace Inventory.Api.Tests.Services;

internal sealed class InventoryClientService : IInventoryClientService
{
    private readonly IRestHttpClientService _restHttpClientService;

    public InventoryClientService(IRestHttpClientService restHttpClientService)
    {
        _restHttpClientService = restHttpClientService;
    }

    public async Task<Guid> PostAsync(AddProductCommand addProductCommand)
    {
        return await _restHttpClientService.PostAsync<AddProductCommand, Guid>("", addProductCommand);
    }
}
