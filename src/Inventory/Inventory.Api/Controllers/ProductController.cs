using Inventory.Application.Features.Products.Commands.AddProduct;
using Inventory.Application.Features.Products.Commands.DeleteProduct;
using Inventory.Application.Features.Products.Commands.UpdateInventory;
using Inventory.Application.Features.Products.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Inventory.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductController : ControllerBase
{
    private readonly IMediator _mediator;

    public ProductController(IMediator mediator)
    {
        _mediator=mediator;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProductVM>>> GetAsync()
    {
        var products = await _mediator.Send(new FindProductsQuery());

        return Ok(products);
    }

    [HttpPost]
    public async Task<ActionResult<Guid>> PostAsync([FromBody] AddProductCommand command)
    {
        var id = await _mediator.Send(command);

        return Ok(id);
    }

    [HttpPut]
    public async Task<ActionResult> PutAsync([FromBody] UpdateInventoryCommand command)
    {
        _ = await _mediator.Send(command);

        return NoContent();
    }

    [HttpDelete]
    public async Task<ActionResult> DeleteAsync(Guid id)
    {
        var command = new DeleteProductCommand 
        { 
            Sku = id 
        };

        _ = await _mediator.Send(command);

        return NoContent(); 
    }
}
