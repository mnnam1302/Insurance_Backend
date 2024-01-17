using backend.DTO.Auth;

namespace backend.Services
{
    public interface ITokenService
    {
        Task<BaseTokenDTO> Login(LoginDTO loginDTO);
        Task<BaseTokenDTO> LoginAdmin(LoginDTO loginDTO);
        Task Logout(string refresh);
        Task<BaseTokenDTO> LoginGoogle(int userId);
        Task<string> Refresh(string refresh);

    }
}
