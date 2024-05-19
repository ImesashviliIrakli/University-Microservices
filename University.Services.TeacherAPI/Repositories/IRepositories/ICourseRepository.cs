using University.Services.TeacherAPI.Models;

namespace University.Services.TeacherAPI.Repositories.IRepositories;

public interface ICourseRepository
{
    Task<IEnumerable<Course>> GetAll();
    Task<Course> GetById(int id);
    Task<List<Course>> GetByUserId(string userId);
    Task<Course> Create(Course teacher);
    Task<Course> Update(Course teacher);
    Task<bool> Delete(int id);
}
