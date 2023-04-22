using AutoMapper;
using Microservices.Services.Catalog.Dtos;
using Microservices.Services.Catalog.Models;

namespace Microservices.Services.Catalog.Mapping
{
    public class EntityMapping : Profile
    {
        public EntityMapping()
        {
            CreateMap<Course, CourseDto>().ReverseMap();
            CreateMap<Category, CategoryDto>().ReverseMap();
            CreateMap<CategoryCreatedDto, CategoryDto>().ReverseMap();
            CreateMap<Feature, FeatureDto>().ReverseMap();

            CreateMap<Course, CourseCreateDto>().ReverseMap();
            CreateMap<Course, CourseUpdateDto>().ReverseMap();


        }
    }
}
