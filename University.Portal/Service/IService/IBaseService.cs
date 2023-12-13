using University.Portal.Models;

namespace University.Portal.Service.IService
{
    public interface IBaseService
    {
        public Task<ResponseDto> SendAsync(RequestDto requestDto, bool withBearer = true);
    }
}
