using Microsoft.EntityFrameworkCore;
using University.Services.TeacherAPI.Data;
using University.Services.TeacherAPI.Models;
using University.Services.TeacherAPI.Repositories.IRepositories;

namespace University.Services.TeacherAPI.Repositories
{
    public class TeacherRepository : ITeacherRepository
    {
        private readonly AppDbContext _db;
        public TeacherRepository(AppDbContext db)
        {

            _db = db;
        }

        public async Task<IEnumerable<Teacher>> GetAll()
        {
            return await _db.Teachers.ToListAsync();
        }

        public async Task<Teacher> GetById(Guid id)
        {
            return await _db.Teachers.FirstOrDefaultAsync(t => t.TeacherId == id);
        }
        public async Task<Teacher> Create(Teacher teacher)
        {
            await _db.Teachers.AddAsync(teacher);
            await _db.SaveChangesAsync();

            return teacher;
        }

        public async Task<Teacher> Update(Teacher teacher)
        {
            try
            {
                _db.Teachers.Update(teacher);
                await _db.SaveChangesAsync();

                return teacher;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<bool> Delete(Guid id)
        {
            try
            {
                var teacher = _db.Teachers.FirstOrDefaultAsync(t => t.TeacherId == id);

                if (teacher == null)
                    return false;

                _db.Teachers.Remove(teacher.Result);
                await _db.SaveChangesAsync();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
