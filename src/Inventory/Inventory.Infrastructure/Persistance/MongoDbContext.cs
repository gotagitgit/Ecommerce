using Inventory.Infrastructure.Dtos;
using Inventory.Infrastructure.Settings;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;

namespace Inventory.Infrastructure.Persistance;

internal class MongoDbContext : IMongoDbContext
{
    public MongoDbContext(IOptions<DatabaseSettings> options)
    {
        var mongoClient = new MongoClient(options.Value.ConnectionString);

        var database = mongoClient.GetDatabase(options.Value.DatabaseName);

        BsonClassMap.RegisterClassMap<ProductDto>(
           map =>
           {
               map.AutoMap();
               map.MapProperty(x => x.Id).SetSerializer(new GuidSerializer(BsonType.String));
           });


        Products = database.GetCollection<ProductDto>(options.Value.CollectionName);
       
        ProductSeedData.SeedData(Products);
    }

    public IMongoCollection<ProductDto> Products { get; }
}
