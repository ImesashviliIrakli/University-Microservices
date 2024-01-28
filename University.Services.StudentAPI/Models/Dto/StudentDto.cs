using System.ComponentModel.DataAnnotations;

namespace University.Services.StudentAPI.Models.Dto;

public class StudentDto
{
    [Key]
    public Guid StudentId { get; set; }
    [Required]
    public Guid UserId { get; set; }
    [Required]
    public string FirstName { get; set; }
    [Required]
    public string LastName { get; set; }
    [Required]
    public string Email { get; set; }
    [Required]
    public DateTime DateOfBirth { get; set; }
    [Required]
    public DateTime EnrollmentDate { get; set; }
    [Required]
    public string Major { get; set; }
}
