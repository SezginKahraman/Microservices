using System.ComponentModel.DataAnnotations;

namespace Microservices.IdentityServer.Dtos
{
    public class SignupDto
    {
        [Required]
        public string Email { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public string City { get; set; }
    }
}
