using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace University.Services.TeacherAPI.Models.Dto;

public class CourseDto
{
    public int Id { get; set; }
    [Required]
    public int CourseId { get; set; }
    [Required]
    public Guid TeacherId { get; set; }
    [ForeignKey("TeacherId")]
    public Teacher Teacher { get; set; }
}
