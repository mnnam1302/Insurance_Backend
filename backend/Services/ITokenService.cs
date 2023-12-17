using backend.DTO;

namespace backend.Services
{
    public interface ITokenService
    {
        Task<TokenDTO?> Login(LoginDTO loginDTO);
        Task<TokenDTO?> LoginGoogle(int userId);

        Task Logout(string refresh);

        Task<string?> Refresh(string refresh);

    }
}
