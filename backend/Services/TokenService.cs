using backend.DTO;
using backend.Repositories;

namespace backend.Services
{
    public class TokenService : ITokenService
    {
        private readonly ITokenRepository _tokenRepository;

        public TokenService(ITokenRepository tokenRepository)
        {
            _tokenRepository = tokenRepository;
        }
        public async Task<TokenDTO?> Login(LoginDTO loginDTO)
        {
            return await _tokenRepository.Login(loginDTO);
        }

        public async Task<string?> Refresh(string refresh)
        {
            return await _tokenRepository.Refresh(refresh);
        }
    }
}
