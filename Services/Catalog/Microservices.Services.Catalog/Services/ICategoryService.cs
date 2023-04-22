using Microservices.Services.Catalog.Dtos;
using Microservices.Services.Catalog.Models;
using Microservices.Shared.Dtos;

namespace Microservices.Services.Catalog.Services
{
    public interface ICategoryService
    {
        Task<Response<CategoryDto>> GetByIdAsync(string id);
        Task<Response<CategoryDto>> CreateAsync(CategoryDto category);
        Task<Response<List<CategoryDto>>> GetAllAsync();
    }
}
