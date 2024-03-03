using System.ComponentModel.DataAnnotations;

namespace Microservices.UI.Models.Catalog.Inputs
{
    public class CourseCreateInput
    {
        [Display(Name = "Resim")]
        public string? Picture { get; set; }

        [Display(Name = "Kullanıcı")]
        public string? UserId { get; set; }

        [Display(Name="Fiyat")]
        public decimal Price { get; set; }

        [Display(Name = "İsim")]
        public string Name { get; set; }

        [Display(Name = "Açıklama")]
        public string Description { get; set; }

        public FeatureCreateInput Feature { get; set; }

        [Display(Name = "Kategori")]
        public string CategoryId { get; set; }

        [Display(Name = "Kurs Resmi")]
        public IFormFile PhotoFile {get; set; }
    }
}
