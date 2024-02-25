using System.ComponentModel.DataAnnotations;

namespace Microservices.UI.Models.Catalog.Inputs
{
    public class CourseUpdateInput
    {
        public string Id { get; set; }

        [Display(Name = "Resim")]
        public string? Picture { get; set; }

        [Display(Name = "Kullanıcı")]
        public string? UserId { get; set; }

        [Display(Name="Fiyat")]
        [Required]
        public int Price { get; set; }

        [Display(Name = "İsim")]
        [Required]
        public string Name { get; set; }

        [Display(Name = "Açıklama")]
        [Required]
        public string Description { get; set; }

        public FeatureUpdateInput Feature { get; set; }

        [Display(Name = "Kategori")]
        [Required]
        public string CategoryId { get; set; }

        [Display(Name = "Kurs Resmi")]
        public IFormFile PhotoFile {get; set; }
    }
}
