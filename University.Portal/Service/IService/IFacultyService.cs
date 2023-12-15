using University.Portal.Models;
using University.Portal.Models.CourseModels;

namespace University.Portal.Service.IService
{
    public interface IFacultyService
    {
        public Task<ResponseDto> GetAllFaculties();
        public Task<ResponseDto> GetFacultyById(int facultyId);
        public Task<ResponseDto> Add(FacultyDto facultyDto);
        public Task<ResponseDto> Update(FacultyDto fsacultyDto);
        public Task<ResponseDto> Delete(int facultyId);
    }
}
