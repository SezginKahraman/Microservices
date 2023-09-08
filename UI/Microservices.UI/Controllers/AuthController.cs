using Microsoft.AspNetCore.Mvc;

namespace Microservices.UI.Controllers
{
    public class AuthController : Controller
    {
        public IActionResult SignIn()
        {
            return View();
        }
    }
}
