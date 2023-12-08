using backend.DTO;
using backend.Services;
using Microsoft.AspNetCore.Mvc;


namespace backend.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class TokenController: ControllerBase
    {
        private readonly ITokenService _tokenService;

        public TokenController(ITokenService tokenService)
        {
            _tokenService = tokenService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginVersion([FromBody] LoginDTO loginDTO)
        {
            if (loginDTO is null)
            {
                return BadRequest("Invalid client request");
            }
            try
            {
                TokenDTO? token = await _tokenService.Login(loginDTO);

                if (token is null)
                {
                    return BadRequest("Login is failed");
                }

                return Ok(new
                {
                    access = token.AccessToken,
                    refresh = token.RefreshToken,
                    user_id = token.UserId
                });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
        }

        [HttpPost("refresh")]
        public async Task<IActionResult> Refresh([FromBody] TokenDTO tokenDTO)
        {
            // Nó bắn vô refresh chắc chắn access đã hết hạn
            if (tokenDTO.RefreshToken is null)
            {
                return BadRequest("Invalid client request");
            }
            try
            {
                string? accessToken = await _tokenService.Refresh(tokenDTO.RefreshToken);
                return Ok(new
                {
                    access = accessToken
                });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
        }
    }
}
