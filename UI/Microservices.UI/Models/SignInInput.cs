using System.ComponentModel;

namespace Microservices.UI.Models
{
    public class SignInInput
    {
        [DisplayName("Email Adresiniz")]
        public string Email { get; set; }

        [DisplayName("Şifreniz")]
        public string Password { get; set; }

        [DisplayName("Beni Hatırla")]
        public string IsRemember { get; set; }
    }
}
