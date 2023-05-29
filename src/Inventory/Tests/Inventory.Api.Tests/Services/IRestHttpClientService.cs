namespace Inventory.Api.Tests.Services;

public interface IRestHttpClientService
{
    Task<TResponse> PostAsync<TRequest, TResponse>(string routeSuffix, TRequest requestContent);
}