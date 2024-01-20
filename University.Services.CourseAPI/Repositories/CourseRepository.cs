using Microsoft.EntityFrameworkCore;
using University.Services.CourseAPI.Data;
using University.Services.CourseAPI.Models;
using University.Services.CourseAPI.Repositories.IRepositories;

namespace University.Services.CourseAPI.Repositories
{
    public class CourseRepository : ICourseRepository
    {
        private readonly AppDbContext _context;
        public CourseRepository(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Course> GetAll()
        {
            return _context.Courses.ToList();
        }

        public Course GetCourse(int courseId)
        {
            return _context.Courses.Include(x => x.Faculty).FirstOrDefault(x => x.CourseId == courseId);
        }

        public Course Add(Course course)
        {
            _context.Courses.Add(course);
            _context.SaveChanges();

            return course;
        }

        public Course Update(Course course)
        {
            _context.Courses.Update(course);
            _context.SaveChanges();

            return course;
        }

        public Course Delete(int courseId)
        {
            Course course = _context.Courses.FirstOrDefault(x => x.CourseId == courseId);

            if(course != null)
            {
                _context.Courses.Remove(course);
                _context.SaveChanges();
            }

            return course;
        }
    }
}
