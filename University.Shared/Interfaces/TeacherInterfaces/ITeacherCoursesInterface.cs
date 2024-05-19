using University.Shared.Dtos.MainDtos;
using University.Shared.Dtos.TeacherDtos;

namespace University.Shared.Interfaces.TeacherInterfaces;

public interface ITeacherCoursesInterface
{
    Task<ResponseDto> GetTeacherCourses();
    Task<ResponseDto> AddTeacherCourses(TeacherCoursesDto teacherCoursesDto);
}
