using Inventory.Application.Features.Products.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Inventory.Api.Controllers
{
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
    }
}
