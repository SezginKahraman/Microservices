using Microservices.Shared.Services;
using Microservices.UI.Models.Catalog.Inputs;
using Microservices.UI.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Microservices.UI.Controllers
{
    [Authorize]
    public class CourseController : Controller
    {
        private readonly ICatalogService _catalogService;
        private readonly ISharedIdentityService _sharedIdentityService;

        public CourseController(ICatalogService catalogService, ISharedIdentityService sharedIdentityService)
        {
            _catalogService = catalogService;
            _sharedIdentityService = sharedIdentityService;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _catalogService.GetAllCourseByUserIdAsync(_sharedIdentityService.GetUserId));
        }

        public async Task<IActionResult> Create()
        {
            var categories = await _catalogService.GetAllCategoriesAsync();

            ViewBag.categoryList = new SelectList(categories, "Id", "Name");

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CourseCreateInput courseCreateInput)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            courseCreateInput.UserId = _sharedIdentityService.GetUserId;

            await _catalogService.CreateCourseAsync(courseCreateInput);

            return RedirectToAction(nameof(Index));

        }

        public async Task<IActionResult> Update(string id)
        {
            var relatedCourse = await _catalogService.GetByCourseId(id);

            if(relatedCourse is null) return RedirectToAction(nameof(Index));

            var categories = await _catalogService.GetAllCategoriesAsync();

            ViewBag.categoryList = new SelectList(categories, "Id", "Name");

            CourseUpdateInput courseUpdateInput = new()
            {
                Id = relatedCourse.Id,
                Name = relatedCourse.Name,
                Price = relatedCourse.Price,
                Feature = new FeatureUpdateInput()
                {
                    Duration = relatedCourse.Feature.Duration
                },
                Picture = relatedCourse.Picture,
                Description = relatedCourse.Description,
                UserId = relatedCourse.UserId,
                CategoryId = relatedCourse.CategoryId
            };

            return View(courseUpdateInput);
        }

        [HttpPost]
        public async Task<IActionResult> Update(CourseUpdateInput courseUpdateInput)
        {
            if (!ModelState.IsValid) return View();

            courseUpdateInput.UserId = _sharedIdentityService.GetUserId;

            await _catalogService.UpdateCourseAsync(courseUpdateInput);

            return RedirectToAction(nameof(Index));

        }

        [HttpGet]
        public async Task<IActionResult> Delete(string id)
        {
            await _catalogService.DeleteCourseAsync(id);

            return RedirectToAction(nameof(Index));

        }
    }
}
