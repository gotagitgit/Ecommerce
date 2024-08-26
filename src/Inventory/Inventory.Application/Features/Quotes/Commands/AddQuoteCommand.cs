using MediatR;

namespace Inventory.Application.Features.Quotes.Commands;

public class AddQuoteCommand : IRequest<Guid>
{
    public Guid CustomerId { get; set; }

    public IReadOnlyList<AddOrderCommand> Orders { get; set; } = [];
}

public class AddOrderCommand : IRequest
{
    public Guid Sku { get; set; }

    public int Quantity { get; set; }
}
