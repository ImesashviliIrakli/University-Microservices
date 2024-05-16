using University.Shared.Dtos.CourseDtos;
using University.Shared.Dtos.MainDtos;
using University.Shared.Interfaces.AuthInterfaces;
using University.Shared.Interfaces.CourseInterfaces;
using University.Shared.Utility;

namespace University.Shared.Services.CourseServices;

public class CourseService : ICourseService
{
    private readonly IBaseService _baseService;
    public CourseService(IBaseService baseService)
    {
        _baseService = baseService;
    }

    public async Task<ResponseDto> GetAllCourses()
    {
        return await _baseService.SendAsync(new RequestDto()
        {
            ApiType = ApiType.GET,
            Url = SD.CourseAPIBase + "/api/Course/"
        });
    }

    public async Task<ResponseDto> GetCourseById(int courseId)
    {
        return await _baseService.SendAsync(new RequestDto()
        {
            ApiType = ApiType.GET,
            Url = SD.CourseAPIBase + $"/api/Course/{courseId}"
        });
    }

    public async Task<ResponseDto> Add(CourseDto courseDto)
    {
        return await _baseService.SendAsync(new RequestDto()
        {
            ApiType = ApiType.POST,
            Data = courseDto,
            Url = SD.CourseAPIBase + "/api/Course/"
        });
    }

    public async Task<ResponseDto> Update(CourseDto courseDto)
    {
        return await _baseService.SendAsync(new RequestDto()
        {
            ApiType = ApiType.PUT,
            Data = courseDto,
            Url = SD.CourseAPIBase + "/api/Course/"
        });
    }

    public async Task<ResponseDto> Delete(int courseId)
    {
        return await _baseService.SendAsync(new RequestDto()
        {
            ApiType = ApiType.DELETE,
            Url = SD.CourseAPIBase + $"/api/Course/{courseId}"
        });
    }
}