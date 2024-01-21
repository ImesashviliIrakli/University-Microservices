using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Win32;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using University.Portal.Models;
using University.Portal.Service.IService;
using University.Portal.Utility;

namespace University.Portal.Controllers
{
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;
        private readonly ITokenProvider _tokenProvider;
        public AuthController(IAuthService authService, ITokenProvider tokenProvider)
        {
            _authService = authService;
            _tokenProvider = tokenProvider;
        }

        [HttpGet]
        public IActionResult Login()
        {
            LoginRequestDto loginRequestDto = new();
            return View(loginRequestDto);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginRequestDto loginRequestDto)
        {
            ResponseDto login = await _authService.LoginAsync(loginRequestDto);

            if (login != null && login.IsSuccess)
            {
                string result = Convert.ToString(login.Result);

                LoginResponseDto loginResponseDto = JsonConvert.DeserializeObject<LoginResponseDto>(result);

                await SignInAsync(loginResponseDto);

                _tokenProvider.SetToken(loginResponseDto.Token);

                return RedirectToAction("Index", "Home");
            }
            else
            {
                TempData["error"] = login.Message;
                return View(loginRequestDto);
            }
        }


        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegistrationRequestDto registrationRequestDto)
        {
            ResponseDto register = await _authService.RegisterAsync(registrationRequestDto);

            if (register != null && register.IsSuccess)
            {
                registrationRequestDto.RoleName = SD.RoleAdmin;

                var assignRole = await _authService.AssignRoleAsync(registrationRequestDto);

                if (assignRole != null && assignRole.IsSuccess)
                {
                    TempData["success"] = "Registration was successful";

                    return RedirectToAction(nameof(Login));
                }
            }
            else
            {
                TempData["error"] = register.Message;
            }

            return View(registrationRequestDto);
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            _tokenProvider.ClearToken();

            return RedirectToAction("Index", "Home");
        }

        [Authorize(Roles = SD.RoleAdmin)]
        public async Task<IActionResult> Users()
        {
            var userDtos = new List<UserDto>();
            ResponseDto responseDto = await _authService.GetUsers();

            if(responseDto.Result != null)
            {
                string resultString = Convert.ToString(responseDto.Result);
                userDtos = JsonConvert.DeserializeObject<List<UserDto>>(resultString);
            }

            return View(userDtos);
        }

        public IActionResult CreateUser()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(RegistrationRequestDto registrationRequestDto)
        {
            ResponseDto register = await _authService.RegisterAsync(registrationRequestDto);

            if (register != null && register.IsSuccess)
            {
                var assignRole = await _authService.AssignRoleAsync(registrationRequestDto);

                if (assignRole != null && assignRole.IsSuccess)
                {
                    TempData["success"] = "New user was added";

                    return RedirectToAction(nameof(Users));
                }
            }
            else
            {
                TempData["error"] = register.Message;
            }

            return View(registrationRequestDto);
        }

        public async Task<IActionResult> UpdateUser(string userId)
        {
            ResponseDto responseDto = await _authService.GetUserById(userId);

            if (responseDto.Result != null)
            {
                string resultString = Convert.ToString(responseDto.Result);
                UserDto userDto = JsonConvert.DeserializeObject<UserDto>(resultString);

                return View(userDto);
            }

            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> UpdateUser(UserDto userDto)
        {
            ResponseDto responseDto = await _authService.UpdateUserAsync(userDto);

            if(responseDto != null && responseDto.IsSuccess)
            {
                TempData["success"] = "User Was updated";
                return RedirectToAction(nameof(Users));
            }

            TempData["error"] = responseDto.Message;
            return View(userDto);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteUser(string userId)
        {
            ResponseDto responseDto = await _authService.DeleteUser(userId);

            if (responseDto != null && responseDto.IsSuccess)
                TempData["success"] = "User Was deleted";
            else            
                TempData["error"] = responseDto.Message;

            return RedirectToAction(nameof(Users));
        }

        private async Task SignInAsync(LoginResponseDto loginResponseDto)
        {

            var handler = new JwtSecurityTokenHandler();

            var jwt = handler.ReadJwtToken(loginResponseDto.Token);

            var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);

            identity.AddClaim(new Claim(JwtRegisteredClaimNames.Email, jwt.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Email).Value));
            identity.AddClaim(new Claim(JwtRegisteredClaimNames.Sub, jwt.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Sub).Value));
            identity.AddClaim(new Claim(JwtRegisteredClaimNames.Name, jwt.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Name).Value));
            identity.AddClaim(new Claim(JwtRegisteredClaimNames.FamilyName, jwt.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.FamilyName).Value));
            identity.AddClaim(new Claim(JwtRegisteredClaimNames.GivenName, jwt.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.GivenName).Value));
            identity.AddClaim(new Claim(JwtRegisteredClaimNames.UniqueName, jwt.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.UniqueName).Value));
            identity.AddClaim(new Claim(ClaimTypes.Name, jwt.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Email).Value));
            identity.AddClaim(new Claim(ClaimTypes.Role, jwt.Claims.FirstOrDefault(x => x.Type == "role").Value));

            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
        }
    }
}
