using Infrastructure.Common;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Inventory.Api.Filters;

public class DatabaseTransactionFilter : IAsyncActionFilter, IAsyncResultFilter
{    
    private readonly IDatabaseContext _databaseContext;
    private bool _isCommit = true;

    public DatabaseTransactionFilter(IDatabaseContext databaseContext)
    {
        _databaseContext = databaseContext;
    }

    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        await _databaseContext.BeginTransactionAsync();

        var callback = await next();

        if (callback.Exception != null)
        {
            await _databaseContext.RollbackTransactionAsync();

            _isCommit = false;
        }
    }

    public async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
    {
        var callback = await next();

        if (callback.Exception == null && _isCommit)
            await _databaseContext.CommitTransactionAsync();
    }
}
