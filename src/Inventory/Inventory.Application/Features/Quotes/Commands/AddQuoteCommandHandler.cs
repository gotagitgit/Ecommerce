using Inventory.Application.Repositories;
using Inventory.Domain.Models;
using MediatR;

namespace Inventory.Application.Features.Quotes.Commands;

internal sealed class AddQuoteCommandHandler : IRequestHandler<AddQuoteCommand, Guid>
{
    private IQuoteRepository _quoteRepository;

    public AddQuoteCommandHandler(IQuoteRepository quoteRepository)
    {
        _quoteRepository = quoteRepository;
    }

    public async Task<Guid> Handle(AddQuoteCommand request, CancellationToken cancellationToken)
    {
        var quoteId = Guid.NewGuid();

        var orders = request.Orders.Select(x => new Order(Guid.NewGuid(), quoteId, x.Sku, x.Quantity)).ToList();

        var quote = new Quote(quoteId, request.CustomerId, DateTime.Now, orders);

        await _quoteRepository.InsertAsync(quote);

        return quoteId;
    }
}
