using University.Services.CourseAPI.Data;
using University.Services.CourseAPI.Models;
using University.Services.CourseAPI.Repositories.IRepositories;

namespace University.Services.CourseAPI.Repositories
{
    public class FacultyRepository : IFacultyRepository
    {
        private readonly AppDbContext _context;
        public FacultyRepository(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Faculty> GetAll()
        {
            return _context.Faculties.ToList();
        }

        public Faculty GetFaculty(int facultyId)
        {
            return _context.Faculties.FirstOrDefault(x => x.FacultyId == facultyId);
        }

        public Faculty Add(Faculty faculty)
        {
            _context.Faculties.Add(faculty);
            _context.SaveChanges();

            return faculty;
        }

        public Faculty Update(Faculty faculty)
        {
            _context.Faculties.Update(faculty);
            _context.SaveChanges();

            return faculty;
        }

        public Faculty Delete(int facultyId)
        {
            Faculty faculty = _context.Faculties.FirstOrDefault(x => x.FacultyId== facultyId);

            if (faculty != null)
            {
                _context.Faculties.Remove(faculty);
                _context.SaveChanges();
            }

            return faculty;
        }
    }
}
