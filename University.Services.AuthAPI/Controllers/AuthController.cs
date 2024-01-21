using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using University.Services.AuthAPI.Models;
using University.Services.AuthAPI.Models.Dto;
using University.Services.AuthAPI.Service.IService;

namespace University.Services.AuthAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IMapper _mapper;
        protected ResponseDto _response;

        public AuthController(IAuthService authService, IMapper mapper)
        {
            _authService = authService;
            _mapper = mapper;
            _response = new();
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegistrationRequestDto model)
        {
            var errorMessage = await _authService.Register(model);

            if (!string.IsNullOrEmpty(errorMessage))
            {
                _response.IsSuccess = false;
                _response.Message = errorMessage;

                return BadRequest(_response);
            }

            return Ok(_response);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto model)
        {
            var loginResponse = await _authService.Login(model);

            if (loginResponse.User == null)
            {
                _response.IsSuccess = false;
                _response.Message = "Username or password is incorrect";

                return BadRequest(_response);
            }

            _response.Result = loginResponse;

            return Ok(_response);
        }

        [HttpPost("assignrole")]
        public async Task<IActionResult> AssignRole([FromBody] RegistrationRequestDto model)
        {
            var roleResponse = await _authService.AssignRole(model.Email, model.RoleName.ToUpper());

            if (!roleResponse)
            {
                _response.IsSuccess = false;
                _response.Message = "Error encountered";

                return BadRequest(_response);
            }

            return Ok(_response);
        }

        [Authorize(Roles = "ADMIN")]
        [HttpGet("getusers")]
        public IActionResult GetUsers()
        {
            var users = _authService.GetUsers();

            if(users == null)
            {
                _response.IsSuccess = false;
                _response.Message = "Could not get users";

                return BadRequest(_response);
            }

            _response.Result = users;

            return Ok(_response);
        }

        [Authorize(Roles = "ADMIN")]
        [HttpGet("getuserbyid/{userId}")]
        public async Task<IActionResult> getUserById(string userId)
        {
            var user = await _authService.GetUserById(userId);

            if (user == null)
            {
                _response.IsSuccess = false;
                _response.Message = "Could not get user";

                return BadRequest(_response);
            }

            UserDto userDto = _mapper.Map<UserDto>(user);

            _response.Result = userDto;

            return Ok(_response);
        }

        [Authorize(Roles = "ADMIN")]
        [HttpPut("updateuser")]
        public async Task<IActionResult> UpdateUser(UserDto userDto)
        {
            var user = await _authService.GetUserById(userDto.ID);

            user.FirstName = userDto.FirstName;
            user.LastName = userDto.LastName;
            user.Email = userDto.Email;
            user.PhoneNumber = userDto.PhoneNumber;
            user.PrivateNumber = userDto.PrivateNumber;

            var updateUser = await _authService.UpdateUser(user);

            _response.IsSuccess = updateUser;

            if (_response.IsSuccess)
                return Ok(_response);
            else
                return BadRequest(_response);
        }

        [Authorize(Roles = "ADMIN")]
        [HttpDelete("deleteuser/{userId}")]
        public async Task<IActionResult> DeleteUser(string userId)
        {
            var deleteUser = await _authService.DeleteUser(userId);

            _response.IsSuccess = deleteUser;

            if (_response.IsSuccess)
                return Ok(_response);
            else
            {
                _response.Message = "Something went wrong";
                return BadRequest(_response);
            }
        }
    }
}
