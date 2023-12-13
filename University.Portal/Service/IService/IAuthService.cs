using University.Portal.Models;

namespace University.Portal.Service.IService
{
    public interface IAuthService
    {
        public Task<ResponseDto> LoginAsync(LoginRequestDto loginRequestDto);
        public Task<ResponseDto> RegisterAsync(RegistrationRequestDto registrationRequestDto);
        public Task<ResponseDto> AssignRoleAsync(RegistrationRequestDto registrationRequestDto);
    }
}
