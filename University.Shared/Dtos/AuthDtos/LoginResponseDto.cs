namespace University.Shared.Dtos.AuthDtos;

public class LoginResponseDto
{
    public UserDto User { get; set; }
    public string Token { get; set; }
}
