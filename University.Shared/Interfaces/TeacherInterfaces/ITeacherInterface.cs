using University.Shared.Dtos.MainDtos;
using University.Shared.Dtos.TeacherDtos;

namespace University.Shared.Interfaces.TeacherInterfaces;

public interface ITeacherInterface
{
    Task<ResponseDto> GetTeacherProfile(string userId);
    Task<ResponseDto> CreateTeacherProfile(TeacherDto teacherDto);
    Task<ResponseDto> UpdateTeacherProfile(TeacherDto teacherDto);
}
