using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace University.Services.TeacherAPI.Models;

public class Course
{
    [Key]
    public int Id { get; set; }
    [Required]
    public int CourseId { get; set; }
    [Required]
    public Guid TeacherId { get; set; }
    [ForeignKey("TeacherId")]
    public Teacher Teacher { get; set; }
}