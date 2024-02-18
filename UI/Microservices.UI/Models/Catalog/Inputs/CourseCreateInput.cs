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
        [Required]
        public int Price { get; set; }

        [Display(Name = "İsim")]
        [Required]
        public string Name { get; set; }

        [Display(Name = "Açıklama")]
        [Required]
        public string Description { get; set; }

        public FeatureCreateInput Feature { get; set; }

        [Display(Name = "Kategori")]
        [Required]
        public string CategoryId { get; set; }
    }
}
