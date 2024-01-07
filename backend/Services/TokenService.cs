using backend.DTO.Auth;
using backend.IRepositories;

namespace backend.Services
{
    public class TokenService : ITokenService
    {
        private readonly ITokenRepository _tokenRepository;

        public TokenService(ITokenRepository tokenRepository)
        {
            _tokenRepository = tokenRepository;
        }
        public async Task<BaseTokenDTO> Login(LoginDTO loginDTO)
        {
            return await _tokenRepository.Login(loginDTO);
        }

        public async Task<BaseTokenDTO> LoginGoogle(int userId)
        {
            return await _tokenRepository.LoginGoogle(userId);
        }

        public async Task Logout(string refresh)
        {
            await _tokenRepository.Logout(refresh);
        }

        public async Task<string> Refresh(string refresh)
        {
            return await _tokenRepository.Refresh(refresh);
        }
    }
}
