using University.Portal.Models;
using University.Portal.Service.IService;
using University.Portal.Utility;
using static University.Portal.Utility.SD;

namespace University.Portal.Service
{
    public class AuthService : IAuthService
    {

        private readonly IBaseService _baseService;
        public AuthService(IBaseService baseService)
        {
            _baseService = baseService;
        }
        public async Task<ResponseDto> AssignRoleAsync(RegistrationRequestDto registrationRequestDto)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = ApiType.POST,
                Data = registrationRequestDto,
                Url = SD.AuthAPIBase + "/api/Auth/assignrole"
            });
        }

        public async Task<ResponseDto> LoginAsync(LoginRequestDto loginRequestDto)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = ApiType.POST,
                Data = loginRequestDto,
                Url = SD.AuthAPIBase + "/api/Auth/login"
            }, withBearer: false);
        }

        public async Task<ResponseDto> RegisterAsync(RegistrationRequestDto registrationRequestDto)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = ApiType.POST,
                Data = registrationRequestDto,
                Url = SD.AuthAPIBase + "/api/Auth/register"
            }, withBearer: false);
        }

        public async Task<ResponseDto> GetUsers()
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = ApiType.GET,
                Url = SD.AuthAPIBase + "/api/Auth/getusers"
            });
        }

        public async Task<ResponseDto> GetUserById(string userId)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = ApiType.GET,
                Url = SD.AuthAPIBase + $"/api/Auth/getuserbyid/{userId}"
            });
        }

        public async Task<ResponseDto> UpdateUserAsync(UserDto userDto)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = ApiType.PUT,
                Data = userDto,
                Url = SD.AuthAPIBase + "/api/Auth/updateuser"
            });
        }

        public async Task<ResponseDto> DeleteUser(string userId)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = ApiType.DELETE,
                Url = SD.AuthAPIBase + $"/api/Auth/deleteuser/{userId}"
            });
        }
    }
}
