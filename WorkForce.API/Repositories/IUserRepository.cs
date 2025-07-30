using WorkForce.API.Models.Responses;
using WorkForce.API.Models.User;

namespace WorkForce.API.Repositories
{
    public interface IUserRepository
    {
        Task<ApiResponse<int>> CreateUserAsync(UserCreateDto userDto);
    }
}
