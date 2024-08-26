using Inventory.Application.Features.Quotes.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Inventory.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class QuotesController : Controller
{
    private readonly IMediator _mediator;

    public QuotesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<ActionResult<Guid>> PostAsync([FromBody] AddQuoteCommand command)
    {
        var id = await _mediator.Send(command);

        return Ok(id);
    }
}
