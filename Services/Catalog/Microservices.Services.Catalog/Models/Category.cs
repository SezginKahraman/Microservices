using MongoDB.Bson.Serialization.Attributes;

namespace Microservices.Services.Catalog.Models
{
    public class Category
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)] // in mongo db, id's are stored as objectId.
        public string Id { get; set; }

        public string Name { get; set; }
    }
}
