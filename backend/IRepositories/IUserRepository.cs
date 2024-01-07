using backend.DTO.User;
using backend.Models;

namespace backend.IRepositories
{
    public interface IUserRepository : IGenericRepository<User>
    {
        //Task<User?> GetUserById(int userId);
        Task<User?> GetUserByEmail(string email);

        Task<User?> GetUserByCardIdentication(string identification);

        Task<User?> CreateUser(User user);
    }
}
