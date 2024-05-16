using System.ComponentModel.DataAnnotations;

namespace University.Shared.Dtos.AuthDtos;

public class LoginRequestDto
{
    [Required]
    public string UserName { get; set; }
    [Required]
    public string Password { get; set; }
}
