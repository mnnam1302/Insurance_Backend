using backend.DTO;
using backend.Models;
using backend.Repositories;

namespace backend.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<User?> GetUserByEmail(string email)
        {
           return await _userRepository.GetUserByEmail(email);
        }

        public async Task<User?> GetUserById(int userId)
        {
            return await _userRepository.GetUserById(userId);
        }

        public async Task<User?> Register(RegisterDTO registerDTO)
        {
            var existsUserByEmail = await _userRepository.GetUserByEmail(registerDTO.Email);

            if (existsUserByEmail != null)
            {
                throw new ArgumentException("Email already exists");
            }

            return await _userRepository.Create(registerDTO);
        }
    }
}
