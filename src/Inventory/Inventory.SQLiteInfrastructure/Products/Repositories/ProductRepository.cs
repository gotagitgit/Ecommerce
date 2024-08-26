using Infrastructure.Common;
using Inventory.Application.Repositories;
using Inventory.Domain.Models;
using Inventory.SQLiteInfrastructure.Persistance;
using Inventory.SQLiteInfrastructure.Products.Dtos;
using Inventory.SQLiteInfrastructure.Products.Factories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Immutable;

namespace Inventory.SQLiteInfrastructure.Products.Repositories;

internal class ProductRepository(IUnitOfWork<SQLiteDbContext> unitOfWork) : IProductRepository
{
    private readonly IUnitOfWork<SQLiteDbContext> _unitOfWork = unitOfWork;

    public async Task DeleteAsync(Guid id)
    {
        await _unitOfWork.ExecuteReadOnlyTransactionAsync(async x =>
        {
            var productDto = await GetProductByIdAsync(x.DbContext, id);

            if (productDto == null)
                return;

            x.DbContext.Products.Remove(productDto);

            _ = await x.DbContext.SaveChangesAsync();
        });
    }

    public async Task<ImmutableList<Product>> FindAsync()
    {
        return await _unitOfWork.ExecuteReadWriteTransactionAsync(async x =>
        {
            var products = await x.DbContext.Products.ToListAsync();

            return products.Select(ProductFactory.ToModel).ToImmutableList();
        });        
    }

    public async Task<Product> GetAsync(Guid id)
    {
        return await _unitOfWork.ExecuteReadWriteTransactionAsync(async x =>
        {
            var productDto = await GetProductByIdAsync(x.DbContext, id);

            if (productDto == null)
                return Product.Empty;

            return ProductFactory.ToModel(productDto);
        });
    }

    public async Task InsertAsync(Product product)
    {
        await _unitOfWork.ExecuteReadOnlyTransactionAsync(async x =>
        {
            var dto = ProductFactory.ToDto(product);

            _ = await x.DbContext.Products.AddAsync(dto);

            _ = await x.DbContext.SaveChangesAsync();
        });        
    }

    public async Task UpdateAsync(Product product)
    {
        await _unitOfWork.ExecuteReadOnlyTransactionAsync(async x =>
        {
            var dto = ProductFactory.ToDto(product);

            x.DbContext.Products.Update(dto);

            _ = await x.DbContext.SaveChangesAsync();
        });
    }

    private static async Task<ProductDto?> GetProductByIdAsync(SQLiteDbContext dbContext, Guid id)
    {
        return await dbContext.Products.SingleOrDefaultAsync(x => x.Id == id);
    }
}
