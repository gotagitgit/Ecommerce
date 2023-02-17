using Inventory.Infrastructure.Dtos;
using MongoDB.Driver;

namespace Inventory.Infrastructure.Persistance;

public interface IMongoDbContext
{
    IMongoCollection<ProductDto> Products { get; }
}