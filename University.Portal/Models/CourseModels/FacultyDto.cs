using System.ComponentModel.DataAnnotations;

namespace University.Portal.Models.CourseModels
{
    public class FacultyDto
    {
        public int FacultyId { get; set; }

        [Required]
        public string FacultyName { get; set; }

        [Required]
        public string FacultyDescription { get; set; }

        [Required]
        public string Degree { get; set; }

        [Required]
        public int SemesterCount { get; set; }
    }
}
