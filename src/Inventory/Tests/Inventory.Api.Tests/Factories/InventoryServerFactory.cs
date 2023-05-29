using Divergic.Logging.Xunit;
using Inventory.Api.Tests.Fixtures;
using Inventory.Infrastructure.Persistance;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
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
            services.AddScoped<ITestMongoDbContext, DbContextFixture>();

            services.AddScoped<IMongoDbContext>(x => x.GetRequiredService<ITestMongoDbContext>());
        });

        base.ConfigureWebHost(builder);
    }

    //protected override void Dispose(bool disposing)
    //{
    //    var db = base.Services.GetRequiredService<IMongoDbContext>();
        
    //    //db.Dispose();

    //    base.Dispose(disposing);
    //}
}
