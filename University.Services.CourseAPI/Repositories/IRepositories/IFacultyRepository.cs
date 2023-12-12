using University.Services.CourseAPI.Models;

namespace University.Services.CourseAPI.Repositories.IRepositories
{
    public interface IFacultyRepository
    {
        public IEnumerable<Faculty> GetAll();
        public Faculty GetFaculty(int facultyId);
        public Faculty Add(Faculty faculty);
        public Faculty Update(Faculty faculty);
        public Faculty Delete(int facultyId);
    }
}
