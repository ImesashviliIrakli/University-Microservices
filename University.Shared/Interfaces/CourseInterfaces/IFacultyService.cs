using University.Shared.Dtos.MainDtos;
using University.Shared.Dtos.CourseDtos;

namespace University.Shared.Interfaces.CourseInterfaces;

public interface IFacultyService
{
    public Task<ResponseDto> GetAllFaculties();
    public Task<ResponseDto> GetFacultyById(int facultyId);
    public Task<ResponseDto> Add(FacultyDto facultyDto);
    public Task<ResponseDto> Update(FacultyDto fsacultyDto);
    public Task<ResponseDto> Delete(int facultyId);
}
