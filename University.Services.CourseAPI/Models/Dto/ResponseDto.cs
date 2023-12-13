namespace University.Services.CourseAPI.Models.Dto
{
    public class ResponseDto
    {
        public object Result { get; set; }
        public bool IsSuccess { get; set; } = true;
        public string Message { get; set; } = "Successful request";
    }
}
