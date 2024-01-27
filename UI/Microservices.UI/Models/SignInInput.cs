using System.ComponentModel.DataAnnotations;

namespace Microservices.UI.Models
{
    public class SignInInput
    {
        [Display(Name = "Email adresiniz")]
        public string Email { get; set; }

        [Display(Name = "şifreniz")]
        public string Password { get; set; }

        [Display(Name = "Beni Hatırla")]
        public bool  IsRemember{ get; set; }
    }
}
