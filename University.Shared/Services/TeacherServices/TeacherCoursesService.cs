using University.Shared.Dtos.MainDtos;
using University.Shared.Dtos.TeacherDtos;
using University.Shared.Interfaces.AuthInterfaces;
using University.Shared.Interfaces.TeacherInterfaces;
using University.Shared.Utility;

namespace University.Shared.Services.TeacherServices;

public class TeacherCoursesService : ITeacherCoursesInterface
{
    private readonly IBaseService _baseService;
    public TeacherCoursesService(IBaseService baseService)
    {
        _baseService = baseService;
    }

    public async Task<ResponseDto> AddTeacherCourses(TeacherCoursesDto teacherCoursesDto)
    {
        return await _baseService.SendAsync(new RequestDto()
        {
            ApiType = ApiType.POST,
            Data = teacherCoursesDto,
            Url = SD.TeacherAPIBase + "/api/Course/"
        });
    
    }

    public async Task<ResponseDto> GetTeacherCourses()
    {
        return await _baseService.SendAsync(new RequestDto()
        {
            ApiType = ApiType.GET,
            Url = SD.TeacherAPIBase + "/api/Course/GetByUserId"
        });
    }
}
