namespace Microservices.UI.Models.Catalog
{
    public class CourseViewModel
    {
        //First install mongodb.driver, after that set up for the classes
        public string Id { get; set; }

        public int Price { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string ShortDescription
        {
            get => Description.Length > 100 ? Description.Substring(0, 100) + "..." : Description;
        }

        public string UserId { get; set; }

        public string Picture { get; set; }

        public string StockPictureUrl { get; set; }

        public DateTime Created { get; set; }

        public DateTime Updated { get; set; }

        public FeatureViewModel Feature { get; set; }

        public string CategoryId { get; set; }

        public CategoryViewModel Category { get; set; }
    }
}
