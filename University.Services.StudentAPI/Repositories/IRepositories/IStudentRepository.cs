using University.Services.StudentAPI.Models;

namespace University.Services.StudentAPI.Repositories.IRepositories
{
    public interface IStudentRepository
    {
        Task<IEnumerable<Student>> GetStudents();
        Task<Student> GetStudentById(Guid studentId);
        Task<Student> GetStudentByEmail(string email);
        Task<Student> AddStudent(Student student);
        Task<Student> UpdateStudent(Student student);
        Task<bool> DeleteStudent(Guid studentId);
    }
}
