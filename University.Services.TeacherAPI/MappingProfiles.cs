using AutoMapper;
using University.Services.TeacherAPI.Models;
using University.Services.TeacherAPI.Models.Dto;

namespace University.Services.TeacherAPI;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<Teacher, TeacherDto>().ReverseMap();
        CreateMap<Course, CourseDto>().ReverseMap();

    }
}
