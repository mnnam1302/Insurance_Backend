using backend.DTO;
using System.Runtime.CompilerServices;

namespace backend.Repositories
{
    public interface ITokenRepository
    {
        Task<TokenDTO?> Login(LoginDTO loginDTO);
        Task<TokenDTO?> LoginGoogle(int userId);

        Task Logout(string refresh);

        Task<string?> Refresh(string refresh);

    }
}
