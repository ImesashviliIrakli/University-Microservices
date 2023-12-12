using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace University.Services.CourseAPI.Models
{
    public class Course
    {
        [Key]
        public int CourseId { get; set; }
        public string CourseName { get; set; }
        public string CourseDescription { get; set; }
        public int Semester { get; set; }
        public int FacultyId { get; set; }

        [ForeignKey("FacultyId")]
        public Faculty Faculty { get; set; }
    }
}
