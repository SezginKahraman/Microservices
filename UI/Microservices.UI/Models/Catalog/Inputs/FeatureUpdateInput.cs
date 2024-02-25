using System.ComponentModel.DataAnnotations;
namespace Microservices.UI.Models.Catalog.Inputs
{
    public class FeatureUpdateInput
    {
        [Required]
        [Display(Name = "Resim")]
        public int Duration { get; set; }
    }
}
