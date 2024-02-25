using Microservices.UI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Microservices.UI.Exceptions;
using Microservices.UI.Services.Interfaces;
using Microsoft.AspNetCore.Diagnostics;

namespace Microservices.UI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ICatalogService _catalogService;

        public HomeController(ILogger<HomeController> logger, ICatalogService catalogService)
        {
            _logger = logger;
            _catalogService = catalogService;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _catalogService.GetAllCoursesAsync());
        }

        public async Task<IActionResult> Detail(string id)
        {
            return View(await _catalogService.GetByCourseId(id));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            var errorFeature = HttpContext.Features.Get<IExceptionHandlerFeature>();
            if (errorFeature != null && errorFeature.Error is UnAuthorizeException)
            {
                
                return RedirectToAction(nameof(AuthController.LogOut), "Auth");
            }

            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}