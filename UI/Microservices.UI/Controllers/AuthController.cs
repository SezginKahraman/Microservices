using Microservices.UI.Models;
using Microservices.UI.Services.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;

namespace Microservices.UI.Controllers
{
    public class AuthController : Controller
    {
        private readonly IIdentityService _identityService;

        public AuthController(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        public IActionResult SignIn()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignIn(SignInInput signInInput)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }


            var response = await _identityService.SignIn(signInInput);

            if (!response.IsSuccessfull)
            {
                response.Errors = new() { "abc", "dsacsa" };
                response.Errors.ForEach(x =>
                {
                    ModelState.AddModelError("dsasacBBc", x); // The string empty because if one the email or password is wrong, dont tell user to which one is wrong. Collect them in to one string in UI.
                });

                return View();

            }

            return RedirectToAction(nameof(Index), "Home");
        }

        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            await _identityService.RevokeRefreshToken();
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }
    }
}
