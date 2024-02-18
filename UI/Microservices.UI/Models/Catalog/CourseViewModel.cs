namespace Microservices.UI.Models.Catalog
{
    public class CourseViewModel
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

        public FeatureViewModel Feature { get; set; }

        public string CategoryId { get; set; }

        public CategoryViewModel CategoryViewModel { get; set; }
    }
}
