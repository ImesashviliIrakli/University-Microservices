using University.Services.CourseAPI.Models;

namespace University.Services.CourseAPI.Repositories.IRepositories
{
    public interface ICourseRepository
    {
        public IEnumerable<Course> GetAll();
        public Course GetCourse(int courseId);
        public Course Add(Course course);
        public Course Update(Course course);
        public Course Delete(int courseId);
    }
}
