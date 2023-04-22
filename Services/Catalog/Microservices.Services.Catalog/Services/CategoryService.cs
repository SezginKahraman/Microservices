using AutoMapper;
using Microservices.Services.Catalog.Dtos;
using Microservices.Services.Catalog.Models;
using Microservices.Services.Catalog.Settings;
using Microservices.Shared.Dtos;
using MongoDB.Driver;

namespace Microservices.Services.Catalog.Services
{
    internal class CategoryService : ICategoryService
    {
        private readonly IMongoCollection<Category> _categoryCollection;
        private readonly IMapper _mapper;

        public CategoryService(IMapper mapper, IDatabaseSettings databaseSettings)
        {
            var client = new MongoClient(databaseSettings.ConnectionStrings);
            var database = client.GetDatabase(databaseSettings.DatabaseName);

            _categoryCollection = database.GetCollection<Category>(databaseSettings.CategoryCollectionName);
            _mapper = mapper;
        }

        public async Task<Response<List<CategoryDto>>> GetAllAsync()
        {
            var categories = await _categoryCollection.Find(category => true).ToListAsync();
            return Response<List<CategoryDto>>.Success(_mapper.Map<List<CategoryDto>>(categories), 200);
        }
        // Refactore category to CategoryCreateCommand or CategoryCreateDto or CategoryCommand.
        public async Task<Response<CategoryDto>> CreateAsync(CategoryDto categoryDto)
        {
            var category = _mapper.Map<Category>(categoryDto);
            await _categoryCollection.InsertOneAsync(category);

            return Response<CategoryDto>.Success(_mapper.Map<CategoryDto>(category), 200);
        }

        public async Task<Response<CategoryDto>> GetByIdAsync(string id)
        {
            var category = await _categoryCollection.Find(category => category.Id == id).FirstOrDefaultAsync();

            if(category == null)
            {
                return Response<CategoryDto>.Fail("Category not found", 404);
            }
            
            return Response<CategoryDto>.Success(_mapper.Map<CategoryDto>(category), 200);
        }
    }
}
