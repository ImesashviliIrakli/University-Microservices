using University.Portal.Models;

namespace University.Portal.Service.IService
{
    public interface IAuthService
    {
        public Task<ResponseDto> LoginAsync(LoginRequestDto loginRequestDto);
        public Task<ResponseDto> RegisterAsync(RegistrationRequestDto registrationRequestDto);
        public Task<ResponseDto> AssignRoleAsync(RegistrationRequestDto registrationRequestDto);
        public Task<ResponseDto> GetUsers();
        public Task<ResponseDto> GetUserById(string userId);
        public Task<ResponseDto> UpdateUserAsync(UserDto userDto);
        public Task<ResponseDto> DeleteUser(string userId);
    }
}
