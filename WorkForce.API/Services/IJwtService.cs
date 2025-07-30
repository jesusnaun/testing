using WorkForce.API.Models.Auth;

namespace WorkForce.API.Services
{
    public interface IJwtService
    {
        string GenerateToken(UserInfo user);
    }
}
