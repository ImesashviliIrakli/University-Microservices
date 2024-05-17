using University.Shared.Dtos.MainDtos;
using University.Shared.Dtos.TeacherDtos;
using University.Shared.Interfaces.AuthInterfaces;
using University.Shared.Interfaces.TeacherInterfaces;
using University.Shared.Utility;

namespace University.Shared.Services.TeacherServices;

public class TeacherService : ITeacherInterface
{
    private readonly IBaseService _baseService;
    public TeacherService(IBaseService baseService)
    {
        _baseService = baseService;
    }

    public async Task<ResponseDto> CreateTeacherProfile(TeacherDto teacherDto)
    {
        return await _baseService.SendAsync(new RequestDto()
        {
            ApiType = ApiType.POST,
            Data = teacherDto,
            Url = SD.TeacherAPIBase + "/api/Teacher/"
        });
    }

    public async Task<ResponseDto> GetTeacherProfile(string userId)
    {
        return await _baseService.SendAsync(new RequestDto()
        {
            ApiType = ApiType.GET,
            Url = SD.TeacherAPIBase + "/api/Teacher/GetByUserId/" + userId
        });
    }

    public async Task<ResponseDto> UpdateTeacherProfile(TeacherDto teacherDto)
    {
        return await _baseService.SendAsync(new RequestDto()
        {
            ApiType = ApiType.PUT,
            Data = teacherDto,
            Url = SD.TeacherAPIBase + "/api/Teacher/"
        });
    }
}
