using Microservices.Shared.Core_3_1.Dtos;
using Microservices.UI.Models;
using Microservices.UI.Models.Catalog;
using Microservices.UI.Models.Catalog.Inputs;
using Microservices.UI.Services.Interfaces;
using System.Net.Http.Json;

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
            var courseRespnose = await _httpClient.PostAsJsonAsync<CourseCreateInput>("courses", courseCreateInput);

            return courseRespnose.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateCourseAsync(CourseUpdateInput courseUpdateInput)
        {
            var courseRespnose = await _httpClient.PutAsJsonAsync<CourseUpdateInput>("courses", courseUpdateInput);

            return courseRespnose.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteCourseAsync(string courseId)
        {
            var courseRespnose = await _httpClient.DeleteAsync($"courses/{courseId}");

            return courseRespnose.IsSuccessStatusCode;
        }

        public async Task<List<CourseViewModel>> GetAllCoursesAsync()
        {
            // https:localhost:5000/services/catalog/courses
            var courseRespnose = await _httpClient.GetAsync("courses");

            if (!courseRespnose.IsSuccessStatusCode)
            {
                return null;
            }

            var successResponse = await courseRespnose.Content.ReadFromJsonAsync<Response<List<CourseViewModel>>>();

            return successResponse.Data;
        }
       
        public async Task<List<CourseViewModel>> GetAllCourseByUserIdAsync(string userId)
        {
            // https:localhost:5000/services/catalog/courses
            var courseRespnose = await _httpClient.GetAsync($"courses/getAllByUserId/{userId}");

            if (!courseRespnose.IsSuccessStatusCode)
            {
                return null;
            }

            var successResponse = await courseRespnose.Content.ReadFromJsonAsync<Response<List<CourseViewModel>>>();

            return successResponse.Data;
        }

        public async Task<CourseViewModel> GetByCourseId(string courseId)
        {
            // https:localhost:5000/services/catalog/courses
            var courseRespnose = await _httpClient.GetAsync($"courses/{courseId}");

            if (!courseRespnose.IsSuccessStatusCode)
            {
                return null;
            }

            var successResponse = await courseRespnose.Content.ReadFromJsonAsync<Response<CourseViewModel>>();

            return successResponse.Data;
        }

        public async Task<List<CategoryViewModel>> GetAllCategoriesAsync()
        {
            // https:localhost:5000/services/catalog/courses
            var catalogResponse = await _httpClient.GetAsync("categories");

            if (!catalogResponse.IsSuccessStatusCode)
            {
                return null;
            }

            var successResponse = await catalogResponse.Content.ReadFromJsonAsync<Response<List<CategoryViewModel>>>();

            return successResponse.Data;
        }

    }
}
