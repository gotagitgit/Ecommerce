using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Inventory.Infrastructure.Dtos;

public sealed class ProductDto
{
    [BsonId]
    //[BsonRepresentation(BsonType.ObjectId)]
    public Guid Id { get; set; }

    [BsonElement("Name")]
    public string Name { get; set; }

    public int Quantity { get; set; }
}
