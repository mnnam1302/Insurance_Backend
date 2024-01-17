using backend.DTO.Auth;
using System.Runtime.CompilerServices;

namespace backend.IRepositories
{
    public interface ITokenRepository
    {
        Task<BaseTokenDTO> Login(LoginDTO loginDTO);

        Task<BaseTokenDTO> LoginGoogle(int userId);

        Task Logout(string refresh);

        Task<string> Refresh(string refresh);

        Task<bool> CheckUserIsAdmin(string email);
    }
}
