namespace University.Shared.Dtos.MainDtos;

public class ResponseDto
{
    public object Result { get; set; }
    public bool IsSuccess { get; set; } = true;
    public string Message { get; set; } = "Successful request";
}