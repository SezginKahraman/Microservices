using Microservices.Shared.Core_3_1.Dtos;
using Microservices.UI.Models;
using Microservices.UI.Models.Catalog;
using Microservices.UI.Models.Catalog.Inputs;
using Microservices.UI.Services.Interfaces;
using System.Net.Http.Json;
using System.Text.Json;

namespace Microservices.UI.Services
{
    public class CatalogService : ICatalogService
    {
        private readonly HttpClient _httpClient;

        public CatalogService(HttpClient client)
        {
            _httpClient = client;
        }

        public async Task<bool> CreateCourseAsync(CourseCreateInput courseCreateInput)
        {
            var courseResponse = await _httpClient.PostAsJsonAsync<CourseCreateInput>("course/addcourse", courseCreateInput);

            return courseResponse.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateCourseAsync(CourseUpdateInput courseUpdateInput)
        {
            // convert response object them later.
            var courseResponse = await _httpClient.PutAsJsonAsync<CourseUpdateInput>("course/updatecourse", courseUpdateInput);

            return courseResponse.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteCourseAsync(string courseId)
        {
            var courseResponse = await _httpClient.DeleteAsync($"course/deletecourse/{courseId}");

            return courseResponse.IsSuccessStatusCode;
        }

        public async Task<List<CourseViewModel>> GetAllCoursesAsync()
        {
            // https:localhost:5000/services/catalog/course
            var courseResponse = await _httpClient.GetAsync("course/getallcourses");

            if (!courseResponse.IsSuccessStatusCode)
            {
                return null;
            }

            var successResponse = await courseResponse.Content.ReadFromJsonAsync<Response<List<CourseViewModel>>>();

            return successResponse.Data;
        }
       
        public async Task<List<CourseViewModel>> GetAllCourseByUserIdAsync(string userId)
        {
            // https:localhost:5000/services/catalog/course
            var courseResponse = await _httpClient.GetAsync($"course/getcoursesbyuserid/{userId}");

            if (!courseResponse.IsSuccessStatusCode)
            {
                return null;
            }

            var successResponse = await courseResponse.Content.ReadFromJsonAsync<Response<List<CourseViewModel>>>();

            return successResponse.Data;
        }

        public async Task<CourseViewModel> GetByCourseId(string courseId)
        {
            // https:localhost:5000/services/catalog/course
            var courseResponse = await _httpClient.GetAsync($"course/getcoursebyid/{courseId}");

            if (!courseResponse.IsSuccessStatusCode)
            {
                return null;
            }

            var successResponse = await courseResponse.Content.ReadFromJsonAsync<Response<CourseViewModel>>();

            return successResponse.Data;
        }

        public async Task<List<CategoryViewModel>> GetAllCategoriesAsync()
        {
            // https:localhost:5000/services/catalog/course
            var catalogResponse = await _httpClient.GetAsync("category/getallcategories");

            if (!catalogResponse.IsSuccessStatusCode)
            {
                return null;
            }

            var successResponse = await catalogResponse.Content.ReadFromJsonAsync<Response<List<CategoryViewModel>>>();

            return successResponse.Data;
        }

    }
}
