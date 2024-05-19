using System.ComponentModel.DataAnnotations;

namespace University.Shared.Dtos.TeacherDtos;

public class TeacherCoursesDto
{
    public int Id { get; set; }
    [Required]
    public int CourseId { get; set; }
    public string CourseName { get; set; }
    public string CourseDescription { get; set; }
    public int Semester { get; set; }
    public int FacultyId { get; set; }
    public string FacultyName { get; set; }
    public Guid UserId { get; set; }
    public TeacherDto Teacher { get; set; }
}
