using Inventory.Application.Repositories;
using Inventory.Domain.Models;
using Inventory.SQLiteInfrastructure.Persistance;
using Inventory.SQLiteInfrastructure.Products.Dtos;
using Inventory.SQLiteInfrastructure.Products.Factories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Immutable;

namespace Inventory.SQLiteInfrastructure.Products.Repositories;

internal class ProductRepository(SQLiteDbContext dbContext) : IProductRepository
{
    private readonly SQLiteDbContext _dbContext = dbContext;

    public async Task DeleteAsync(Guid id)
    {       
        var productDto = await GetProductByIdAsync(id);

        if (productDto == null)
            return;

        _dbContext.Products.Remove(productDto);

        _ = await _dbContext.SaveChangesAsync();       
    }

    public async Task<ImmutableList<Product>> FindAsync()
    {
        var products = await _dbContext.Products.ToListAsync();

        return products.Select(ProductFactory.ToModel).ToImmutableList();
    }

    public async Task<Product> GetAsync(Guid id)
    {
        var productDto = await GetProductByIdAsync(id);

        if (productDto == null)
            return Product.Empty;

        return ProductFactory.ToModel(productDto);
    }

    public async Task InsertAsync(Product product)
    {
        var dto = ProductFactory.ToDto(product);

        _ = await _dbContext.Products.AddAsync(dto);

        _ = await _dbContext.SaveChangesAsync(); 
    }

    public async Task UpdateAsync(Product product)
    {        
        var dto = ProductFactory.ToDto(product);

        _dbContext.Products.Update(dto);

        _ = await _dbContext.SaveChangesAsync();        
    }

    private async Task<ProductDto?> GetProductByIdAsync(Guid id) =>
        await _dbContext.Products.SingleOrDefaultAsync(x => x.Id == id);
    
}
