using AutoMapper;
using Microservices.Services.Catalog.Dtos;
using Microservices.Services.Catalog.Models;
using Microservices.Services.Catalog.Services;
using Microservices.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Microservices.Services.Catalog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : BaseController
    {
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;

        public CategoryController(ICategoryService categoryService, IMapper mapper)
        {
            _categoryService = categoryService;
            _mapper = mapper;
        }

        [Route("getcategorybyid/{id}")]
        [HttpGet]
        public async Task<IActionResult> GetCategoryById(string id)
        {
            var response = await _categoryService.GetByIdAsync(id);

            return CreateActionResultInstance(response);
        }

        [Route("getallcategories")]
        [HttpGet]
        public async Task<IActionResult> GetAllCategories()
        {
            var response = await _categoryService.GetAllAsync();

            return CreateActionResultInstance(response);
        }


        [Route("addcategory")]
        [HttpPost]
        public async Task<IActionResult> Create(CategoryCreatedDto categoryCreateDto)
        {
            var categoryDto = _mapper.Map<CategoryDto>(categoryCreateDto);
            var response = await _categoryService.CreateAsync(categoryDto);

            return CreateActionResultInstance(response);
        }
    }
}
