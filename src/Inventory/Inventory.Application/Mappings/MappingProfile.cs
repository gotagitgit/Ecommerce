using AutoMapper;
using Inventory.Application.Features.Products.Queries;
using Inventory.Domain.Models;

namespace Inventory.Application.Mappings;

public sealed class MappingProfile : Profile
{
	public MappingProfile()
	{
        CreateMap<Product, ProductVM>()
            .ConstructUsing(x => new ProductVM
            {
                SKU = x.SKU,
                Name= x.Name,
                Quantity= x.Quantity,
            })
            .ReverseMap();
    }
}
