using Inventory.Application.Repositories;
using Inventory.Domain.Models;
using Inventory.SQLiteInfrastructure.Dtos;
using Inventory.SQLiteInfrastructure.Persistance;
using Inventory.SQLiteInfrastructure.Quotes.Factories;
using Microsoft.EntityFrameworkCore;

namespace Inventory.SQLiteInfrastructure.Quotes.Repositories;

internal sealed class QuoteRepository(SQLiteDbContext dbContext) : IQuoteRepository
{
    private readonly SQLiteDbContext DbContext = dbContext;

    public async Task DeleteAsync(Guid id)
    {
        var dto = await GetQuoteByIdAsync(id);

        if (dto == null)
            return;

        DbContext.Quotes.Remove(dto);

        _ = await DbContext.SaveChangesAsync();
    }

    public async Task<IReadOnlyList<Quote>> FindAsync()
    {       
        var quotes = await DbContext.Quotes.ToListAsync();

        var ordersDto = await DbContext.Orders.ToListAsync();

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

        var ordersDto = DbContext.Orders.Where(x => x.QuoteId == quoteDto.Id).ToList();

        return QuoteFactory.ToModel(quoteDto, ordersDto);   
    }

    public async Task InsertAsync(Quote quote)
    {       
        var quoteDto = QuoteFactory.ToDto(quote);

        var ordersDto = quote.Orders.Select(QuoteFactory.ToDto).ToList();

        _ = await DbContext.Quotes.AddAsync(quoteDto);

        await DbContext.Orders.AddRangeAsync(ordersDto);

        _ = await DbContext.SaveChangesAsync();    
    }

    public async Task UpdateAsync(Quote quote)
    {        
        var quoteDto = QuoteFactory.ToDto(quote);

        var ordersDto = quote.Orders.Select(QuoteFactory.ToDto).ToList();

        _ = DbContext.Quotes.Update(quoteDto);

        DbContext.Orders.UpdateRange(ordersDto);

        _ = await DbContext.SaveChangesAsync();      
    }

    private async Task<QuoteDto?> GetQuoteByIdAsync(Guid id) =>
        await DbContext.Quotes.SingleOrDefaultAsync(x => x.Id == id);    
}
