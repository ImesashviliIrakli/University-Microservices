using System.ComponentModel.DataAnnotations;

namespace University.Services.CourseAPI.Models
{
    public class Faculty
    {
        [Key]
        public int FacultyId { get; set; }
        public string FacultyName { get; set; }
        public string FacultyDescription { get; set; }
        public string Degree { get; set; }
        public int SemesterCount { get; set; }
    }
}
