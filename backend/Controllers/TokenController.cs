using Azure.Core;
using backend.Attribute;
using backend.DTO;
using backend.Models;
using backend.Services;
using Microsoft.AspNetCore.Mvc;


namespace backend.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class TokenController: ControllerBase
    {
        private readonly ITokenService _tokenService;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IUserService _userService;

        public TokenController(ITokenService tokenService, 
                                IHttpClientFactory httpClientFactory, 
                                IUserService userService)
        {
            _tokenService = tokenService;
            _httpClientFactory = httpClientFactory;
            _userService = userService;
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

        [HttpPost("validate-google-token")]
        public async Task<IActionResult> ValidateGoogleToken([FromBody] GoogleTokenDTO googleTokenDTO)
        {
            if (googleTokenDTO == null)
            {
                return BadRequest(new { Error = "Email is not valid! Let's check" });
            }
            try
            {
                // Check xem email có trong hệ thống không
                User? user = await _userService.GetUserByEmail(googleTokenDTO.Email);

                if (user == null)
                {
                    return NotFound("Email is not valid! Let's check");
                }

                var httpClient = _httpClientFactory.CreateClient();
                string credential_token = googleTokenDTO.CredentialToken;

                // Gửi yêu cầu xác thực đến Google API
                var googleResponse = await httpClient.GetAsync($"https://www.googleapis.com/oauth2/v3/tokeninfo?id_token={credential_token}");

                // Kiểm tra xác thực thành công từ Google
                if (googleResponse.IsSuccessStatusCode)
                {
                    var userData = await googleResponse.Content.ReadAsStringAsync();

                    // Thực hiện xử lý với thông tin người dùng, tạo access và refresh cho user, v.v.
                    // ... Không cần check user ở đây vì ở trên check rồi
                    TokenDTO? token = await _tokenService.LoginGoogle(user.UserId);

                    if (token is null)
                    {
                        return BadRequest("Login is failed");
                    }

                    // Gửi phản hồi về frontend
                    return Ok(new
                    {
                        access = token.AccessToken,
                        refresh = token.RefreshToken,
                        user_id = token.UserId
                    });
                }
                else
                {
                    return Unauthorized(new { success = false, message = "Invalid access token." });
                }
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
        }
    }
}
