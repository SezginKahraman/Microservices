using Microservices.Services.Catalog.Models;
using MongoDB.Bson.Serialization.Attributes;

namespace Microservices.Services.Catalog.Dtos
{
    public class CourseDto
    {
        //First install mongodb.driver, after that set up for the classes
        public string Id { get; set; }

        public int Price { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string UserId { get; set; }

        public string Picture { get; set; }

        public DateTime Created { get; set; }

        public DateTime Updated { get; set; }

        public FeatureDto Feature { get; set; }

        public string CategoryId { get; set; }

        public CategoryDto Category { get; set; }
    }
}
