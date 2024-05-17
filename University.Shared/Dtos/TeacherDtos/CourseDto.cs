using System.ComponentModel.DataAnnotations;

namespace University.Shared.Dtos.TeacherDtos;

public class CourseDto
{
    public int Id { get; set; }
    [Required]
    public int CourseId { get; set; }
    public Guid TeacherId { get; set; }

    public TeacherDto Teacher { get; set; }
}
