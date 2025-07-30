using WorkForce.API.Models.Auth;

namespace WorkForce.API.Repositories
{
    public interface IAuthRepository
    {
        Task<UserInfo?> ValidateUserAsync(string username);
    }
}
