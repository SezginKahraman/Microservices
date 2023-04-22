using Microservices.Services.Catalog.Dtos;
using Microservices.Services.Catalog.Models;
using Microservices.Shared.Dtos;

namespace Microservices.Services.Catalog.Services
{
    public interface ICourseService
    {
        Task<Response<List<CourseDto>>> GetAllAsync();

        Task<Response<CourseDto>> CreateAsync(CourseCreateDto course);

        Task<Response<NoContent>> UpdateAsync(CourseUpdateDto course);

        Task<Response<NoContent>> DeleteAsync(string id);

        Task<Response<CourseDto>> GetByIdAsync(string id);

        Task<Response<List<CourseDto>>> GetByUserIdAsync(string userId);
    }
}
