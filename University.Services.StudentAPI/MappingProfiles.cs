using AutoMapper;

namespace University.Services.StudentAPI;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<Models.Student, Models.Dto.StudentDto>().ReverseMap();
    }
}
