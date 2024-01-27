using System.ComponentModel.DataAnnotations;

namespace Microservices.UI.Models
{
    public class SignInInput
    {
        [Required]
        [Display(Name = "Email adresiniz")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "şifreniz")]
        public string Password { get; set; }

        [Required]
        [Display(Name = "Beni Hatırla")]
        public bool  IsRemember{ get; set; }
    }
}
