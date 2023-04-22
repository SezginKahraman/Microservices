using Microservices.Services.Catalog.Dtos;
using Microservices.Services.Catalog.Services;
using Microservices.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Microservices.Services.Catalog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : BaseController
    {
        private readonly ICourseService _courseService;
        public CourseController(ICourseService courseService)
        {
            _courseService = courseService;
        }
        
        [Route("getcoursesbyuserid/{id}")]
        [HttpGet]
        public async Task<IActionResult> GetCoursesByUserId(string id)
        {
            var response = await _courseService.GetByIdAsync(id);

            return CreateActionResultInstance(response);
        }

        [Route("getallcourses")]
        [HttpGet]
        public async Task<IActionResult> GetAllCourses()
        {
            var response = await _courseService.GetAllAsync();

            return CreateActionResultInstance(response);
        }

        [Route("getcoursebyid/{id}")]
        [HttpGet]
        public async Task<IActionResult> GetCourseById(string id)
        {
            var response = await _courseService.GetByIdAsync(id);

            return CreateActionResultInstance(response);
        }

        [Route("addcourse")]
        [HttpPost]
        public async Task<IActionResult> Create(CourseCreateDto courseCreateDto)
        {
            var response = await _courseService.CreateAsync(courseCreateDto);

            return CreateActionResultInstance(response);
        }

        [Route("updatecourse")]
        [HttpPut]
        public async Task<IActionResult> Update(CourseUpdateDto courseUpdateDto)
        {
            var response = await _courseService.UpdateAsync(courseUpdateDto);

            return CreateActionResultInstance(response);
        }

        [Route("deletecourse")]
        [HttpDelete]
        public async Task<IActionResult> Delete(string id)
        {
            var response = await _courseService.DeleteAsync(id);

            return CreateActionResultInstance(response);
        }
    }
}
