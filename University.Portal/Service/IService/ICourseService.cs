using University.Portal.Models;
using University.Portal.Models.CourseModels;

namespace University.Portal.Service.IService
{
    public interface ICourseService
    {
        public Task<ResponseDto> GetAllCourses();
        public Task<ResponseDto> GetCourseById(int courseId);
        public Task<ResponseDto> Add(CourseDto courseDto);
        public Task<ResponseDto> Update(CourseDto courseDto);
        public Task<ResponseDto> Delete(int courseId);
    }
}
