using University.Shared.Dtos.MainDtos;

namespace University.Shared.Interfaces.AuthInterfaces;

public interface IBaseService
{
    public Task<ResponseDto> SendAsync(RequestDto requestDto, bool withBearer = true);

}
