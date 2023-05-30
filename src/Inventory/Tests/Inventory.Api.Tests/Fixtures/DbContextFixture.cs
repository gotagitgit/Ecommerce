using Inventory.Infrastructure.Dtos;
using Inventory.Infrastructure.Persistance;
using Inventory.Infrastructure.Settings;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Inventory.Api.Tests.Fixtures;

internal sealed class DbContextFixture : IMongoDbContext, IDisposable
{
    private readonly IOptions<DatabaseSettings> _databaseSettings;

    public DbContextFixture(IOptions<DatabaseSettings> databaseSettings)
    {
        _databaseSettings = databaseSettings;

        var databaseConnectionString = databaseSettings.Value.ConnectionString;

        var databaseName = databaseSettings.Value.DatabaseName;

        var mongoClient = new MongoClient(databaseConnectionString);

        var database = mongoClient.GetDatabase(databaseName);

        var collectionName = databaseSettings.Value.CollectionName;

        Products = database.GetCollection<ProductDto>(collectionName);
    }

    public IMongoCollection<ProductDto> Products { get; }

    public void Dispose()
    {
        var databaseConnectionString = _databaseSettings.Value.ConnectionString;

        var client = new MongoClient(databaseConnectionString);

        client.DropDatabase(_databaseSettings.Value.DatabaseName);
    }
}
