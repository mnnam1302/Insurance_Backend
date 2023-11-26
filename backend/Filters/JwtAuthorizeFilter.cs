using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace backend.Filters
{
    public class JwtAuthorizeFilter: IAuthorizationFilter
    {
        private readonly IConfiguration _config;

        public JwtAuthorizeFilter(IConfiguration config)
        {
            _config = config;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            // Lấy token từ header Authorization của request
            var token = context.HttpContext.Request
                .Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

            // Nếu token không tồn tại, trả về lỗi Unauthorized
            if (token == null)
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            // Tạo đối tượng xử lý token
            var tokenHandler = new JwtSecurityTokenHandler();
            //var key = Encoding.ASCII.GetBytes(_config["Jwt:AccessTokenSecret"] ?? "");
            var key = Encoding.ASCII.GetBytes(_config["Jwt:SecreteKey"] ?? "");

            try
            {
                // Tạo đối tượng xử lý token
                var claimsPrincipal = tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,

                    // Nếu token hết hạn thì gọi phương thức ValidateToken,
                    // mã lỗi SecurityExpiredException sẽ được throw ra
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                var accessToken = (JwtSecurityToken)validatedToken;

                // Kiểm tra xem token có hết hạn không - Kiểm tra lại có cần check ở đây không, nếu làm như vậy sẽ gãy token
                // Tạm thời command lại ở đây
                if (accessToken.ValidTo < DateTime.UtcNow)
                {
                    // Token đã hết hạn - Access token hết hạn - Nếu hết hạn thì ở đây nó đứng luôn
                    // Xử lý lỗi hoặc đăng nhập lại
                    context.Result = new UnauthorizedResult();
                    return;
                }

                // Lấy thông tin user ID từ token và đặt vào HttpContext.Items
                var userId = int.Parse(accessToken.Claims.First().Value);
                context.HttpContext.Items["UserId"] = userId;
            }
            catch (Exception)
            {
                context.Result = new UnauthorizedResult();
                return;
            }
        }
    }
}
