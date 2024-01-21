using AutoMapper;
using University.Services.AuthAPI.Models;
using University.Services.AuthAPI.Models.Dto;

namespace University.Services.CourseAPI
{
    public class MapperProfiles : Profile
    {
        public MapperProfiles()
        {
            CreateMap<ApplicationUser, UserDto>().ReverseMap();
        }
    }
}
