using Infrastructure.Common;
using Inventory.Application.Repositories;
using Inventory.Domain.Models;
using Inventory.SQLiteInfrastructure.Dtos;
using Inventory.SQLiteInfrastructure.Persistance;
using Inventory.SQLiteInfrastructure.Quotes.Factories;
using Microsoft.EntityFrameworkCore;

namespace Inventory.SQLiteInfrastructure.Quotes.Repositories;

internal sealed class QuoteRepository(IUnitOfWork<SQLiteDbContext> unitOfWork) : IQuoteRepository
{
    private readonly IUnitOfWork<SQLiteDbContext> _unitOfWork = unitOfWork;

    public async Task DeleteAsync(Guid id)
    {
        await _unitOfWork.ExecuteReadOnlyTransactionAsync(async x =>
        {
            var dto = await GetQuoteByIdAsync(x.DbContext, id);

            if (dto == null)
                return;

            x.DbContext.Quotes.Remove(dto);

            _ = await x.DbContext.SaveChangesAsync();
        });
    }

    public async Task<IReadOnlyList<Quote>> FindAsync()
    {
        return await _unitOfWork.ExecuteReadWriteTransactionAsync(async x =>
        {
            var quotes = await x.DbContext.Quotes.ToListAsync();

            var ordersDto = await x.DbContext.Orders.ToListAsync();

            return quotes.Select(x =>
            {
                var orders = ordersDto.Where(y => y.QuoteId == x.Id).ToList();

                return QuoteFactory.ToModel(x, orders);
            }).ToList();
        });
    }

    public async Task<Quote> GetAsync(Guid id)
    {
        return await _unitOfWork.ExecuteReadWriteTransactionAsync(async x =>
        {
            var quoteDto = await GetQuoteByIdAsync(x.DbContext, id);

            if (quoteDto == null)
                return Quote.Empty;

            var ordersDto = x.DbContext.Orders.Where(x => x.QuoteId == quoteDto.Id).ToList();

            return QuoteFactory.ToModel(quoteDto, ordersDto);
        });
    }

    public async Task InsertAsync(Quote quote)
    {
        await _unitOfWork.ExecuteReadOnlyTransactionAsync(async x =>
        {
            var quoteDto = QuoteFactory.ToDto(quote);

            var ordersDto = quote.Orders.Select(QuoteFactory.ToDto).ToList();

            _ = await x.DbContext.Quotes.AddAsync(quoteDto);

            await x.DbContext.Orders.AddRangeAsync(ordersDto);

            _ = await x.DbContext.SaveChangesAsync();
        });
    }

    public async Task UpdateAsync(Quote quote)
    {
        await _unitOfWork.ExecuteReadOnlyTransactionAsync(async x =>
        {
            var quoteDto = QuoteFactory.ToDto(quote);

            var ordersDto = quote.Orders.Select(QuoteFactory.ToDto).ToList();

            _ = x.DbContext.Quotes.Update(quoteDto);

            x.DbContext.Orders.UpdateRange(ordersDto);

            _ = await x.DbContext.SaveChangesAsync();
        });
    }

    private static async Task<QuoteDto?> GetQuoteByIdAsync(SQLiteDbContext dbContext, Guid id)
    {
        return await dbContext.Quotes.SingleOrDefaultAsync(x => x.Id == id);
    }
}
