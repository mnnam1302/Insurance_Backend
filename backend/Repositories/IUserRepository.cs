using backend.DTO;
using backend.Models;

namespace backend.Repositories
{
    public interface IUserRepository
    {
        Task<User?> GetUserById(int userId);
        Task<User?> GetUserByEmail(string email);
        Task<User?> Create(RegisterDTO registerDTO);
        Task<User?> UpdateUserById(UserDTO userDTO);
    }
}
