using backend.DTO;
using backend.Models;

namespace backend.Services
{
    public interface IUserService
    {
        Task<User?> Register(RegisterDTO registerDTO);

        Task<User?> GetUserById(int userId);

        Task<User?> GetUserByEmail(string email);
    }
}
