using University.Shared.Dtos.CourseDtos;
using University.Shared.Dtos.MainDtos;
using University.Shared.Interfaces.AuthInterfaces;
using University.Shared.Interfaces.CourseInterfaces;
using University.Shared.Utility;

namespace University.Shared.Services.CourseServices;

public class FacultyService : IFacultyService
{
    private readonly IBaseService _baseService;
    public FacultyService(IBaseService baseService)
    {
        _baseService = baseService;
    }

    public async Task<ResponseDto> GetAllFaculties()
    {
        return await _baseService.SendAsync(new RequestDto()
        {
            ApiType = ApiType.GET,
            Url = SD.CourseAPIBase + "/api/Faculty/"
        });
    }

    public async Task<ResponseDto> GetFacultyById(int facultyId)
    {
        return await _baseService.SendAsync(new RequestDto()
        {
            ApiType = ApiType.GET,
            Url = SD.CourseAPIBase + $"/api/Faculty/{facultyId}"
        });
    }

    public async Task<ResponseDto> Add(FacultyDto facultyDto)
    {
        return await _baseService.SendAsync(new RequestDto()
        {
            ApiType = ApiType.POST,
            Data = facultyDto,
            Url = SD.CourseAPIBase + "/api/Faculty/"
        });
    }

    public async Task<ResponseDto> Update(FacultyDto facultyDto)
    {
        return await _baseService.SendAsync(new RequestDto()
        {
            ApiType = ApiType.PUT,
            Data = facultyDto,
            Url = SD.CourseAPIBase + "/api/Faculty/"
        });
    }

    public async Task<ResponseDto> Delete(int facultyId)
    {
        return await _baseService.SendAsync(new RequestDto()
        {
            ApiType = ApiType.DELETE,
            Url = SD.CourseAPIBase + $"/api/Faculty/{facultyId}"
        });
    }
}