using Microsoft.EntityFrameworkCore;
using University.Services.StudentAPI.Data;
using University.Services.StudentAPI.Models;
using University.Services.StudentAPI.Repositories.IRepositories;

namespace University.Services.StudentAPI.Repositories;

public class StudentRepository : IStudentRepository
{
    private readonly AppDbContext _db;

    public StudentRepository(AppDbContext db)
    {
        _db = db;
    }

    public async Task<IEnumerable<Student>> GetStudents()
    {
        return await _db.Students.ToListAsync();
    }

    public async Task<Student> GetStudentById(Guid studentId)
    {
        return await _db.Students.FirstOrDefaultAsync(s => s.StudentId == studentId);
    }

    public async Task<Student> GetStudentByEmail(string email)
    {
        return await _db.Students.FirstOrDefaultAsync(s => s.Email == email);
    }

    public async Task<Student> AddStudent(Student student)
    {
        await _db.Students.AddAsync(student);
        await _db.SaveChangesAsync();
        return student;
    }

    public async Task<Student> UpdateStudent(Student student)
    {
        _db.Students.Update(student);
        await _db.SaveChangesAsync();
        return student;
    }

    public async Task<bool> DeleteStudent(Guid studentId)
    {
        try
        {
            var student = await GetStudentById(studentId);
            if (student == null)
            {
                return false;
            }

            _db.Students.Remove(student);
            await _db.SaveChangesAsync();
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }
}
