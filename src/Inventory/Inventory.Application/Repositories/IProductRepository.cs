using Inventory.Domain.Models;
using System.Collections.Immutable;

namespace Inventory.Application.Repositories
{
    public interface IProductRepository
    {
        Task DeleteAsync(Guid id);
        Task<ImmutableList<Product>> FindAsync();
        Task<Product> GetAsync(Guid id);
        Task InsertAsync(Product product);
        Task UpdateAsync(Product product);
    }
}