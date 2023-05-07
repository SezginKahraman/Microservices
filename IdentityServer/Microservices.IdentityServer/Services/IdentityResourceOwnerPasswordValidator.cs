using IdentityModel;
using IdentityServer4.Validation;
using Microservices.IdentityServer.Models;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Microservices.IdentityServer.Services
{
    public class IdentityResourceOwnerPasswordValidator : IResourceOwnerPasswordValidator
    {
        private readonly UserManager<ApplicationUser> userManager;

        public IdentityResourceOwnerPasswordValidator(UserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager;
        }

        public async Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
        {
            var existUser = await userManager.FindByEmailAsync(context.UserName);

            if (existUser == null)
            {
                var errors = new Dictionary<string, object>();

                errors.Add("errors", new List<string>() { "email veya şifreniz yanlış." });

                context.Result.CustomResponse = errors;

                return;
            }
            var passwordCheck = await userManager.CheckPasswordAsync(existUser, context.Password);

            if (!passwordCheck)
            {
                var errors = new Dictionary<string, object>();
                errors.Add("errors", new List<string>() { "email veya şifre yanlış."});
                context.Result.CustomResponse = errors;
                return;
            }
            context.Result = new GrantValidationResult(existUser.Id.ToString(), OidcConstants.AuthenticationMethods.Password);
        }
    }
}
