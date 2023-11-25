using backend.DTO;

namespace backend.Services
{
    public interface ITokenService
    {
        Task<TokenDTO?> Login(LoginDTO loginDTO);

        Task<string?> Refresh(string refresh);
    }
}
