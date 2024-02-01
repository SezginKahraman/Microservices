namespace Microservices.UI.Models.Catalog.Inputs
{
    public class CourseCreateInput
    {
        public int Price { get; set; }

        public string Description { get; set; }

        public string UserId { get; set; }

        public string Picture { get; set; }

        public FeatureCreateInput Feature { get; set; }

        public string CategoryId { get; set; }
    }
}
