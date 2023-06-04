using AutoMapper;
using CourseServices.Catalog.Dtos;
using CourseServices.Catalog.Models;

namespace CourseServices.Catalog.Mapping
{
    public class GeneralMapping: Profile
    {
        public GeneralMapping()
        {
            CreateMap<Courses,CourseDto>().ReverseMap();
            CreateMap<Category,CategoryDto>().ReverseMap();
            CreateMap<Feature, FeatureDto>().ReverseMap();

            CreateMap<Courses, CourseCreateDto>().ReverseMap();
            CreateMap<Courses, CourseUpdateDto>().ReverseMap();


        }

    }
}
