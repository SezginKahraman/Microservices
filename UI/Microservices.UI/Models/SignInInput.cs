using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Microservices.UI.Models
{
    public class SignInInput
    {
        [Required]
        [DisplayName("Email Adresiniz")]
        public string Email { get; set; }

        [Required]
        [DisplayName("Şifreniz")]
        public string Password { get; set; }

        [Required]
        [DisplayName("Beni Hatırla")]
        public bool IsRemember { get; set; }
    }
}
