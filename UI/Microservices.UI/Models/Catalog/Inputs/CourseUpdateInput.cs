namespace Microservices.UI.Models.Catalog.Inputs
{
    public class CourseUpdateInput
    {
        public string Id { get; set; }

        public int Price { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Picture { get; set; }

        public string UserId { get; set; }

        public FeatureUpdateInput Feature { get; set; }

        public string CategoryId { get; set; }
    }
}
