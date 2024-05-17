using University.Services.TeacherAPI.Models;

namespace University.Services.TeacherAPI.Repositories.IRepositories;

public interface ITeacherRepository
{
    Task<IEnumerable<Teacher>> GetAll();
    Task<Teacher> GetById(Guid id);
    Task<Teacher> GetByUserId(Guid userId);
    Task<Teacher> Create(Teacher teacher);
    Task<Teacher> Update(Teacher teacher);
    Task<bool> Delete(Guid id);
}
