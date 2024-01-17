using backend.DTO.Auth;
using backend.IRepositories;
using backend.Models;
using Firebase.Auth;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace backend.Repositories
{
    public class TokenRepository : ITokenRepository
    {
        private readonly InsuranceDbContext _dbContext;
        private readonly IConfiguration _config;
        private readonly IUserRepository _userRepository;

        public TokenRepository(
            InsuranceDbContext context,
            IConfiguration config,
            IUserRepository userRepository)
        {
            _dbContext = context;
            _config = config;
            _userRepository = userRepository;
        }

        private string GenerateToken(int userId, TimeSpan expirationTime, string secretKeyConfig)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_config[$"Jwt:{secretKeyConfig}"] ?? "");

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, userId.ToString()),
                    //new Claim(ClaimTypes.Name, user.Username)
                }),
                Expires = DateTime.UtcNow.Add(expirationTime),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var jwt = tokenHandler.WriteToken(token);
            return jwt;
        }

        private async Task<Token?> GetExistingRefreshToken(int userId)
        {
            // Kiểm tra xem có người dùng có refresh token cũ hay là lần đầu tiên
            return await _dbContext.Tokens.FirstOrDefaultAsync(t => t.UserId == userId);
        }

        private async Task CreateOrUpdateRefreshToken(int userId, string refreshToken)
        {
            // Kiểm tra xem người dùng đã có refresh token hay chưa
            Token? existingToken = await GetExistingRefreshToken(userId);

            if (existingToken != null)
            {
                // Nếu đã có refresh token, cập nhật giá trị mới
                existingToken.TokenValue = refreshToken;
                existingToken.Created = DateTime.Now;
                existingToken.Expired = DateTime.Now.AddDays(1); // Cập nhật thời gian hết hạn
            }
            else
            {
                // Nếu chưa có refresh token, thêm mới
                var newToken = new Token
                {
                    TokenValue = refreshToken,
                    Created = DateTime.Now,
                    Expired = DateTime.Now.AddDays(1),
                    UserId = userId
                };

                // Thêm token mới vào DbContext và lưu vào cơ sở dữ liệu
                _dbContext.Tokens.Add(newToken);
            }

            // Lưu thay đổi vào cơ sở dữ liệu
            await _dbContext.SaveChangesAsync();
        }

        public async Task<BaseTokenDTO> Login(LoginDTO loginDTO)
        {
            try
            {
                // Đoạn này sẽ gọi 1 procedure trong SQL - Check User có tồn tại
                string sql = "EXEC dbo.CheckLogin @email, @password";
                IEnumerable<Models.User> result = await _dbContext.Users.FromSqlRaw(sql,
                    new SqlParameter("@email", loginDTO.Email),
                    new SqlParameter("@password", loginDTO.Password)
                    ).ToListAsync();

                var user = result.FirstOrDefault();

                if (user != null)
                {
                    // Tạo access token && refresh token
                    var accessToken = GenerateToken(user.UserId, TimeSpan.FromMinutes(15), "SecreteKey");
                    var refreshToken = GenerateToken(user.UserId, TimeSpan.FromMinutes(20), "SecreteKey");

                    // Lưu refresh token cho người dùng
                    await CreateOrUpdateRefreshToken(user.UserId, refreshToken);

                    // Return access_token và refresh_token, maybe userId
                    return new BaseTokenDTO
                    {
                        Type = "Bearer",
                        Access = accessToken,
                        Refresh = refreshToken,
                        UserId = user.UserId,
                        Email = user.Email,
                        IsAdmin = user.IsAdmin
                    };
                }
                else
                {
                    throw new Exception("Wrong email or password");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<string> Refresh(string refresh)
        {
            // Tạo đối tượng xử lý refresh token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_config["Jwt:SecreteKey"] ?? "");

            try
            {
                // Tạo đối tượng xử lý token
                var claimsPrincipal = tokenHandler.ValidateToken(refresh, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,

                    // Nếu token hết hạn thì gọi phương thức ValidateToken,
                    // Mã lỗi SecurityExpiredException sẽ được throw ra
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                var refreshToken = (JwtSecurityToken)validatedToken;

                // Kiểm tra userId có tồn tại không
                var userId = int.Parse(refreshToken.Claims.First().Value);

                //var user = await _userRepository.GetUserById(userId);
                var user = await _userRepository.Get(userId);

                if (user == null)
                {
                    throw new Exception("User is not valid");
                }

                var accessToken = GenerateToken(userId, TimeSpan.FromMinutes(5), "SecreteKey");

                return accessToken;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task Logout(string refresh)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_config["Jwt:SecreteKey"] ?? "");

            try
            {
                var claimsPrincipal = tokenHandler.ValidateToken(refresh, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                var refreshToken = (JwtSecurityToken)validatedToken;

                // Kiểm tra userId có tồn tại không
                var userId = int.Parse(refreshToken.Claims.First().Value);

                //var user = await _userRepository.GetUserById(userId);
                var user = await _userRepository.Get(userId);

                if (user == null)
                {
                    throw new Exception("User is not valid");
                }

                // Xóa refresh token ở database
                // Update refresh về "" || null nếu tồn tại

                // Kiểm tra xem người dùng đã có refresh token hay chưa
                Token? existingToken = await GetExistingRefreshToken(userId);

                if (existingToken == null)
                {
                    throw new Exception("Token is not valid in database");
                }
                else
                {
                    existingToken.TokenValue = "";          // Update thuộc tính refresh ""
                    existingToken.Expired = DateTime.Now;   // Update expired Now => sure
                }

                // Lưu thay đổi vào cơ sở dữ liệu
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<BaseTokenDTO> LoginGoogle(int userId)
        {
            try
            {
                // Tạo access token && refresh token
                var accessToken = GenerateToken(userId, TimeSpan.FromMinutes(5), "SecreteKey");
                var refreshToken = GenerateToken(userId, TimeSpan.FromMinutes(20), "SecreteKey");

                // Lưu refresh token cho người dùng
                await CreateOrUpdateRefreshToken(userId, refreshToken);

                // Return access_token và refresh_token, maybe userId
                return new BaseTokenDTO
                {
                    Type = "Bearer",
                    Access = accessToken,
                    Refresh = refreshToken
                };
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> CheckUserIsAdmin(string email)
        {
            try
            {
                var isAdmin = await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == email && u.IsAdmin == true);
                return isAdmin != null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
