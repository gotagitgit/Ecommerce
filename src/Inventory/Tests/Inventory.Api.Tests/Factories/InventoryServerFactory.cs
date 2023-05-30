using Divergic.Logging.Xunit;
using Inventory.Api.Tests.Fixtures;
using Inventory.Infrastructure.Persistance;
using Inventory.Infrastructure.Settings;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Xunit.Abstractions;

namespace Inventory.Api.Tests.Factories;

public sealed class InventoryServerFactory : WebApplicationFactory<Program>
{
    private readonly ITestOutputHelper _testOutputHelper;

    public InventoryServerFactory(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            services.AddLogging(x => x.AddXunit(_testOutputHelper, new LoggingConfig { LogLevel = LogLevel.Debug }));
        });

        builder.ConfigureTestServices(services =>
        {
            services.AddScoped<IMongoDbContext, DbContextFixture>();

            services.Configure<DatabaseSettings>(x => x.DatabaseName = $"inventory_test_db_{Guid.NewGuid()}");
        });

        base.ConfigureWebHost(builder);
    }
}
