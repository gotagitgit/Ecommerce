using MediatR;
using System.Collections.Immutable;

namespace Inventory.Application.Features.Products.Queries;

public sealed class FindProductsQuery : IRequest<ImmutableList<ProductVM>>
{
	public FindProductsQuery()
	{
	} 
}
