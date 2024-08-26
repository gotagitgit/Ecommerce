using Inventory.Domain.Models;
using Inventory.SQLiteInfrastructure.Dtos;

namespace Inventory.SQLiteInfrastructure.Quotes.Factories;

internal sealed class QuoteFactory
{
    public static Quote ToModel(QuoteDto dto, IReadOnlyList<OrderDto> ordersDto)
    {
        var orders = ordersDto.Select(ToModel).ToList();

        return new Quote(dto.Id, dto.CustomerId, dto.DateCreated, orders);
    }

    public static QuoteDto ToDto(Quote quote) => new() { Id = quote.Id, CustomerId = quote.CustomerId, DateCreated = quote.DateCreated };

    public static OrderDto ToDto(Order orderDto) => new() { 
                                                                Id = orderDto.Id,
                                                                QuoteId = orderDto.QuoteId, 
                                                                Sku = orderDto.Sku, 
                                                                Quantity = orderDto.Quantity 
                                                              };

    private static Order ToModel(OrderDto dto) => new(dto.Id, dto.QuoteId, dto.Sku, dto.Quantity);
}
