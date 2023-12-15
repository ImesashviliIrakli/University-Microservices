using System.ComponentModel.DataAnnotations;

namespace University.Portal.Models.CourseModels
{
    public class CourseDto
    {
        public int CourseId { get; set; }

        [Required]
        public string CourseName { get; set; }
        [Required]
        public string CourseDescription { get; set; }
        [Required]
        public int Semester { get; set; }
        [Required]
        public int FacultyId { get; set; }
        public FacultyDto Faculty { get; set; }
    }
}
