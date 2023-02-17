using AutoMapper;
using Inventory.Application.Repositories;
using MediatR;
using System.Collections.Immutable;

namespace Inventory.Application.Features.Products.Queries;

public sealed class FindProductsQueryHandler : IRequestHandler<FindProductsQuery, ImmutableList<ProductVM>>
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;

    public FindProductsQueryHandler(
        IProductRepository productRepository,
        IMapper mapper)
    {
        _productRepository = productRepository;
        _mapper = mapper;
    }

    public async Task<ImmutableList<ProductVM>> Handle(FindProductsQuery request, CancellationToken cancellationToken)
    {
        var products = await _productRepository.FindAsync();                

        var mappedProducts = _mapper.Map<List<ProductVM>>(products);

        return mappedProducts.ToImmutableList();
    }
}
