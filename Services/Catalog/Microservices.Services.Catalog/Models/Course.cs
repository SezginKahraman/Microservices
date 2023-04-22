using MongoDB.Bson.Serialization.Attributes;

namespace Microservices.Services.Catalog.Models
{
    public class Course
    {
        //First install mongodb.driver, after that set up for the classes

        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)] 
        public string Id { get; set; }

        [BsonRepresentation(MongoDB.Bson.BsonType.Decimal128)]
        public int Price { get; set; }
        
        public string Name { get; set; }

        public string Description { get; set; }

        public string UserId { get; set; }

        public string Picture { get; set; }

        [BsonRepresentation(MongoDB.Bson.BsonType.DateTime)] 
        public DateTime Created { get; set; }

        [BsonRepresentation(MongoDB.Bson.BsonType.DateTime)]
        public DateTime Updated { get; set; }

        public Feature Feature{ get; set; }
        
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)] 
        public string CategoryId { get; set; }

        [BsonIgnore]
        public Category Category { get; set; }

    }
}
