using Inventory.Infrastructure.Dtos;
using Inventory.Infrastructure.Persistance;
using Inventory.Infrastructure.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Inventory.Api.Tests.Fixtures;

internal sealed class DbContextFixture : ITestMongoDbContext
{
    private readonly string _databaseConnectionString;

    private readonly string _databaseName;

    private readonly MongoClient _mongoClient;

    public DbContextFixture(IOptions<DatabaseSettings> databaseSettings)
    {
        var _databaseConnectionString = databaseSettings.Value.ConnectionString;

        var _databaseName = $"inventory_test_db_{Guid.NewGuid()}";

        var _mongoClient = new MongoClient(_databaseConnectionString);

        var database = _mongoClient.GetDatabase(_databaseName);

        var collectionName = databaseSettings.Value.CollectionName;

        Products = database.GetCollection<ProductDto>(collectionName);
    }

    public IMongoCollection<ProductDto> Products { get; }

    public void Dispose()
    {
        var client = new MongoClient(_databaseConnectionString);

        client.DropDatabase(_databaseName);
    }
}
