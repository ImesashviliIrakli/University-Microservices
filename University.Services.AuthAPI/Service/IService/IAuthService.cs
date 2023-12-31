﻿using University.Services.AuthAPI.Models.Dto;

namespace University.Services.AuthAPI.Service.IService
{
    public interface IAuthService
    {
        public Task<string> Register(RegistrationRequestDto registrationRequestDto);
        public Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto);
        public Task<bool> AssignRole(string email, string roleName);
    }
}
