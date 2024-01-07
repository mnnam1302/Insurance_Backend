using Azure.Core;
using backend.Attribute;
using backend.DTO.Auth;
using backend.Models;
using backend.Services;
using Microsoft.AspNetCore.Mvc;


namespace backend.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class TokensController: ControllerBase
    {
        private readonly ITokenService _tokenService;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IUserService _userService;

        public TokensController(ITokenService tokenService, 
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
                var result = await _tokenService.Login(loginDTO);
                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { Errors = ex.Message });
            }
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout([FromBody] TokenDTO tokenDTO)
        {
            if (tokenDTO.Refresh is null)
            {
                return BadRequest("Invalid client request");
            }
            try
            {
                await _tokenService.Logout(tokenDTO.Refresh);

                return Ok(new
                {
                    message = "Logout successful"
                });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { Errors = ex.Message });
            }
        }

        [HttpPost("refresh")]
        public async Task<IActionResult> Refresh([FromBody] TokenDTO tokenDTO)
        {
            // Nó bắn vô refresh chắc chắn access đã hết hạn
            if (tokenDTO.Refresh is null)
            {
                return BadRequest("Invalid client request");
            }
            try
            {
                var accessToken = await _tokenService.Refresh(tokenDTO.Refresh);
                return Ok(new
                {
                    access = accessToken
                });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { Errors = ex.Message });
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
                var user = await _userService.GetUserByEmail(googleTokenDTO.Email);

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
                    var result = await _tokenService.LoginGoogle(user.UserId);

                    result.UserId = user.UserId;
                    result.Email = user.Email;

                    if (result is null)
                    {
                        return BadRequest("Login is failed");
                    }

                    return Ok(result);
                }
                else
                {
                    return Unauthorized(new { success = false, message = "Invalid access token." });
                }
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { Errors = ex.Message });
            }
        }
    }
}
