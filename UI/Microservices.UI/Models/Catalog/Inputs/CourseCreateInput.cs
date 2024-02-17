using System.ComponentModel.DataAnnotations;

namespace Microservices.UI.Models.Catalog.Inputs
{
    public class CourseCreateInput
    {
        [Display(Name="Fiyat")]
        [Required]
        public int Price { get; set; }

        [Display(Name = "Fiyat")]
        [Required]
        public string Name { get; set; }

        [Display(Name = "Fiyat")]
        [Required]
        public string Description { get; set; }

        [Display(Name = "Fiyat")]
        [Required]
        public string UserId { get; set; }

        [Display(Name = "Fiyat")]
        [Required]
        public string Picture { get; set; }

        public FeatureCreateInput Feature { get; set; }

        [Display(Name = "Fiyat")]
        [Required]
        public string CategoryId { get; set; }
    }
}
