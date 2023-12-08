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
        public async Task<IActionResult> Login([FromBody] LoginDTO loginDTO)
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

        [HttpPost("logout")]
        public async Task<IActionResult> Logout([FromBody] TokenDTO tokenDTO)
        {
            if (tokenDTO.RefreshToken is null)
            {
                return BadRequest("Invalid client request");
            }
            try
            {
                await _tokenService.Logout(tokenDTO.RefreshToken);

                return Ok(new
                {
                    message = "Logout successful"
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
