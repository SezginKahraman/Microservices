using System.ComponentModel.DataAnnotations;

namespace Microservices.UI.Models.Catalog.Inputs
{
    public class FeatureCreateInput
    {
        [Display(Name = "Süre")]
        public int Duration { get; set; }
    }
}
