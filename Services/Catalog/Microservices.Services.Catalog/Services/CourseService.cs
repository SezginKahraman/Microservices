using AutoMapper;
using Microservices.Services.Catalog.Dtos;
using Microservices.Services.Catalog.Models;
using Microservices.Services.Catalog.Settings;
using Microservices.Shared.Dtos;
using MongoDB.Driver;

namespace Microservices.Services.Catalog.Services
{
    internal class CourseService : ICourseService
    {
        private readonly IMongoCollection<Course> _courseCollection;
        private readonly IMongoCollection<Category> _categoryCollection;
        ICategoryService _categoryService;

        private readonly IMapper _mapper;

        public CourseService(IMapper mapper, IDatabaseSettings databaseSettings, ICategoryService categoryService)
        {
            var client = new MongoClient(databaseSettings.ConnectionStrings);
            var database = client.GetDatabase(databaseSettings.DatabaseName);

            _courseCollection = database.GetCollection<Course>(databaseSettings.CourseCollectionName);
            _categoryCollection = database.GetCollection<Category>(databaseSettings.CategoryCollectionName);
            _mapper = mapper;
            _categoryService = categoryService;
        }

        public async Task<Response<List<CourseDto>>> GetAllAsync()
        {

            var courses = await _courseCollection.Find(category => true).ToListAsync(); // MongoDb is not relational database. 
            // So in order to set the categories that is related with the courses, we have to make this process manually.

            if (courses.Any())
            {
                foreach (var course in courses)
                {
                    //course.Category =  _mapper.Map<Category>(await _categoryService.GetByIdAsync(course.CategoryId));
                    course.Category = await _categoryCollection.Find(x => x.Id == course.CategoryId).FirstOrDefaultAsync();
                }
            }
            else
            {
                courses = new List<Course>();
            }
            
            return Response<List<CourseDto>>.Success(_mapper.Map<List<CourseDto>>(courses), 200);
        }

        // Refactore category to CategoryCreateCommand or CategoryCreateDto or CategoryCommand.
        public async Task<Response<CourseDto>> CreateAsync(CourseCreateDto course)
        {
            var newcourse = _mapper.Map<Course>(course);
            newcourse.Created = DateTime.Now;

            await _courseCollection.InsertOneAsync(newcourse);

            return Response<CourseDto>.Success(_mapper.Map<CourseDto>(newcourse), 200);
        }

        public async Task<Response<NoContent>> UpdateAsync(CourseUpdateDto course)
        {
            var updatedCourse = _mapper.Map<Course>(course);
            updatedCourse.Updated = DateTime.Now;

            var result = await _courseCollection.FindOneAndReplaceAsync(x=> x.Id == updatedCourse.Id, updatedCourse);
            if(result == null)
            {
                return Response<NoContent>.Fail("Course not found", 404);
            }


            return Response<NoContent>.Success(204);
        }

        public async Task<Response<NoContent>> DeleteAsync(string id)
        {
            var result = await _courseCollection.DeleteOneAsync(x => x.Id == id);

            if (result.DeletedCount>0)
            {
                return Response<NoContent>.Success(204);

            }
            return Response<NoContent>.Fail("Course not found", 404);
        }

        public async Task<Response<CourseDto>> GetByIdAsync(string id)
        {
            var course = await _courseCollection.Find(course => course.Id == id).FirstOrDefaultAsync();

            if (course == null)
            {
                return Response<CourseDto>.Fail("Category not found", 404);
            }

            course.Category = await _categoryCollection.Find(x => x.Id == course.CategoryId).FirstAsync();

            return Response<CourseDto>.Success(_mapper.Map<CourseDto>(course), 200);
        }
        public async Task<Response<List<CourseDto>>> GetByUserIdAsync(string userId)
        {
            var courses = await _courseCollection.Find(course => course.UserId == userId).ToListAsync();

            if (courses.Any())
            {
                foreach (var course in courses)
                {
                    //course.Category =  _mapper.Map<Category>(await _categoryService.GetByIdAsync(course.CategoryId));
                    course.Category = await _categoryCollection.Find(x => x.Id == course.CategoryId).FirstOrDefaultAsync();
                }
            }
            else
            {
                courses = new List<Course>();
            }

            return Response<List<CourseDto>>.Success(_mapper.Map<List<CourseDto>>(courses), 200);
        }
    }
}
