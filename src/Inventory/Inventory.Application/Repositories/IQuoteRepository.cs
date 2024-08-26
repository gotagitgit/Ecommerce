using Inventory.Domain.Models;

namespace Inventory.Application.Repositories;

public interface IQuoteRepository
{
    Task DeleteAsync(Guid id);
    Task<IReadOnlyList<Quote>> FindAsync();
    Task<Quote> GetAsync(Guid id);
    Task InsertAsync(Quote quote);
    Task UpdateAsync(Quote quote);
}