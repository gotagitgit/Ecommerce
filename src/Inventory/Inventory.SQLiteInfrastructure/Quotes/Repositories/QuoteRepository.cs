using Inventory.Application.Repositories;
using Inventory.Domain.Models;
using Inventory.SQLiteInfrastructure.Dtos;
using Inventory.SQLiteInfrastructure.Persistance;
using Inventory.SQLiteInfrastructure.Quotes.Factories;
using Microsoft.EntityFrameworkCore;

namespace Inventory.SQLiteInfrastructure.Quotes.Repositories;

internal sealed class QuoteRepository(SQLiteDbContext dbContext) : IQuoteRepository
{
    private readonly SQLiteDbContext _dbContext = dbContext;

    public async Task DeleteAsync(Guid id)
    {
        var dto = await GetQuoteByIdAsync(id);

        if (dto == null)
            return;        

        _dbContext.Quotes.Remove(dto);

        _ = await _dbContext.SaveChangesAsync();
    }

    public async Task<IReadOnlyList<Quote>> FindAsync()
    {       
        var quotes = await _dbContext.Quotes.ToListAsync();

        var ordersDto = await _dbContext.Orders.ToListAsync();

        return quotes.Select(x =>
        {
            var orders = ordersDto.Where(y => y.QuoteId == x.Id).ToList();

            return QuoteFactory.ToModel(x, orders);
        }).ToList();  
    }

    public async Task<Quote> GetAsync(Guid id)
    {        
        var quoteDto = await GetQuoteByIdAsync(id);

        if (quoteDto == null)
            return Quote.Empty;

        var ordersDto = _dbContext.Orders.Where(x => x.QuoteId == quoteDto.Id).ToList();

        return QuoteFactory.ToModel(quoteDto, ordersDto);   
    }

    public async Task InsertAsync(Quote quote)
    {       
        var quoteDto = QuoteFactory.ToDto(quote);

        var ordersDto = quote.Orders.Select(QuoteFactory.ToDto).ToList();

        _ = await _dbContext.Quotes.AddAsync(quoteDto);

        await _dbContext.Orders.AddRangeAsync(ordersDto);

        _ = await _dbContext.SaveChangesAsync();    
    }

    public async Task UpdateAsync(Quote quote)
    {        
        var quoteDto = QuoteFactory.ToDto(quote);

        var ordersDto = quote.Orders.Select(QuoteFactory.ToDto).ToList();

        _ = _dbContext.Quotes.Update(quoteDto);

        _dbContext.Orders.UpdateRange(ordersDto);

        _ = await _dbContext.SaveChangesAsync();      
    }

    private async Task<QuoteDto?> GetQuoteByIdAsync(Guid id) =>
        await _dbContext.Quotes.SingleOrDefaultAsync(x => x.Id == id);    
}
