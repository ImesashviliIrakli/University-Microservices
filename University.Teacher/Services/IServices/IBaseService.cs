using University.Teacher.Models;

namespace University.Teacher.Service.IService
{
    public interface IBaseService
    {
        public Task<ResponseDto> SendAsync(RequestDto requestDto, bool withBearer = true);
    }
}
