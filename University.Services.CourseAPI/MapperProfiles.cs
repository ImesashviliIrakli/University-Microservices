using AutoMapper;
using University.Services.CourseAPI.Models;
using University.Services.CourseAPI.Models.Dto;

namespace University.Services.CourseAPI
{
    public class MapperProfiles : Profile
    {
        public MapperProfiles()
        {
            CreateMap<Faculty, FacultyDto>().ReverseMap();
            CreateMap<Course, CourseDto>().ReverseMap();
        }
    }
}
