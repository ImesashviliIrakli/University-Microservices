using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace University.Services.TeacherAPI.Models;

public class Course
{
    [Key]
    public int Id { get; set; }
    [Required]
    public int CourseId { get; set; }
    public string CourseName { get; set; }
    public string CourseDescription { get; set; }
    public int Semester { get; set; }
    public int FacultyId { get; set; }
    public string FacultyName { get; set; }
    [Required]
    public Guid UserId { get; set; }

    [ForeignKey("UserId")]
    public Teacher Teacher { get; set; }
}