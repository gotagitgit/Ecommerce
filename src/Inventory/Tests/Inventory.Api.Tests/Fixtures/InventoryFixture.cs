using Inventory.Api.Tests.Factories;
using Inventory.Api.Tests.Services;
using Inventory.Infrastructure.Persistance;
using Microsoft.Extensions.DependencyInjection;
using Xunit.Abstractions;

namespace Inventory.Api.Tests.Fixtures
{
    public sealed class InventoryFixture : IDisposable
    {
        private readonly InventoryServerFactory _inventoryFactory;
        private readonly HttpClient _inventoryHttpClient;

        public InventoryFixture(ITestOutputHelper testOutputHelper)
        {
            _inventoryFactory = new InventoryServerFactory(testOutputHelper);

            var _inventoryHttpClient = _inventoryFactory.CreateClient();

            var baseUri = new Uri("http://localhost:5002/api/Product");

            var restHttpClientService = new RestHttpClientService(_inventoryHttpClient, baseUri);

            InventoryClient = new InventoryClientService(restHttpClientService);

            ServiceProvider = _inventoryFactory.Server.Services;
        }

        public IInventoryClientService InventoryClient { get; }

        public IServiceProvider ServiceProvider { get; }

        public void Dispose()
        {
            // Find a way to properly Dispose Mongo DB
            //var xxx = ServiceProvider.GetRequiredService<IMongoDbContext>();//.Dispose();

            _inventoryFactory.Dispose();
        }
    }
}
