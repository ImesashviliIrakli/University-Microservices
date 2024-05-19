using Microsoft.EntityFrameworkCore;
using University.Services.TeacherAPI.Data;
using University.Services.TeacherAPI.Models;
using University.Services.TeacherAPI.Repositories.IRepositories;

namespace University.Services.TeacherAPI.Repositories;

public class CourseRepository : ICourseRepository
{
    private readonly AppDbContext _db;

    public CourseRepository(AppDbContext db)
    {
        _db = db;
    }

    public async Task<IEnumerable<Course>> GetAll()
    {
        return await _db.Courses.ToListAsync();
    }

    public async Task<Course> GetById(int id)
    {
        return await _db.Courses.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<Course> Create(Course course)
    {
        var result = await _db.Courses.AddAsync(course);
        await _db.SaveChangesAsync();
        return result.Entity;
    }

    public async Task<Course> Update(Course course)
    {
        var result = _db.Courses.Update(course);
        await _db.SaveChangesAsync();
        return result.Entity;
    }

    public async Task<bool> Delete(int id)
    {
        try
        {
            var course = await _db.Courses.FirstOrDefaultAsync(x => x.Id == id);
            if (course == null)
                return false;

            _db.Courses.Remove(course);
            await _db.SaveChangesAsync();

            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public async Task<List<Course>> GetByUserId(string userId)
    {
        return await _db.Courses.Where(x => x.UserId.ToString() == userId).ToListAsync();
    }
}
