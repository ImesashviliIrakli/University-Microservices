using University.Services.AuthAPI.Models;

namespace University.Services.AuthAPI.Service.IService
{
    public interface IJwtTokenGenerator
    {
        public string GenerateToken(ApplicationUser applicationUser, IEnumerable<string> roles);
    }
}
