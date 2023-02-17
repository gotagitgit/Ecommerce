using Inventory.Application.Repositories;
using Inventory.Domain.Models;
using Inventory.Infrastructure.Factories;
using Inventory.Infrastructure.Persistance;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using System.Collections.Immutable;

namespace Inventory.Infrastructure.Repositories;

public sealed class ProductRepository : IProductRepository
{
    private readonly IMongoDbContext _mongoDbContext;
    private readonly ILogger<ProductRepository> _logger;

    public ProductRepository(
        IMongoDbContext mongoDbContext,
        ILogger<ProductRepository> logger)
    {
        _mongoDbContext=mongoDbContext;
        _logger=logger;
    }

    public async Task<ImmutableList<Product>> FindAsync()
    {
        var dtos = await (await _mongoDbContext.Products.FindAsync(_ => true)).ToListAsync();

        var products = dtos.Select(x => ProductFactory.ToModel(x));

        return products.ToImmutableList();
    }

    public async Task<Product> GetAsync(Guid id)
    {
        var dto = await (await _mongoDbContext.Products.FindAsync(x => x.Id == id)).FirstAsync();

        if (dto == null)
        {
            _logger.LogError("Product record with {id} not found.", id);
        }

        return ProductFactory.ToModel(dto);
    }

    public async Task InsertAsync(Product product)
    {
        var dto = ProductFactory.ToDto(product);

        await _mongoDbContext.Products.InsertOneAsync(dto);
    }

    public async Task UpdateAsync(Product product)
    {
        var dto = ProductFactory.ToDto(product);

        _ = await _mongoDbContext.Products.ReplaceOneAsync(x => x.Id == dto.Id, dto);
    }

    public async Task DeleteAsync(Guid id)
    {
        _ = await _mongoDbContext.Products.DeleteOneAsync(x => x.Id == id);
    }
}
