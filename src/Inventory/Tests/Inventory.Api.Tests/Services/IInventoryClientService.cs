using Inventory.Application.Features.Products.Commands.AddProduct;

namespace Inventory.Api.Tests.Services;

public interface IInventoryClientService
{
    Task<Guid> PostAsync(AddProductCommand addProductCommand);
}