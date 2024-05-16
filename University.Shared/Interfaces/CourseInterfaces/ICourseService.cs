using University.Shared.Dtos.MainDtos;
using University.Shared.Dtos.CourseDtos;

namespace University.Shared.Interfaces.CourseInterfaces;

public interface ICourseService
{
    public Task<ResponseDto> GetAllCourses();
    public Task<ResponseDto> GetCourseById(int courseId);
    public Task<ResponseDto> Add(CourseDto courseDto);
    public Task<ResponseDto> Update(CourseDto courseDto);
    public Task<ResponseDto> Delete(int courseId);
}
