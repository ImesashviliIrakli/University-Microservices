using University.Shared.Utility;
using static University.Shared.Utility.SD;

namespace University.Shared.Dtos.MainDtos;

public class RequestDto
{
    public ApiType ApiType { get; set; } = ApiType.GET;
    public string Url { get; set; }
    public object Data { get; set; }
    public string AccessToken { get; set; }
    public ContentType ContentType { get; set; } = ContentType.Json;
}
