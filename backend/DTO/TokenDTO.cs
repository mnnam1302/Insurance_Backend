using System.ComponentModel.DataAnnotations;

namespace backend.DTO
{
    public class TokenDTO
    {
        //[Required(ErrorMessage = "Access token must be required")]
        //public string AccessToken { get; set; } = "";

        //[Required(ErrorMessage = "Refresh token must be required")]
        //public string RefreshToken { get; set; } = "";

        //[Required(ErrorMessage = "User ID must be required")]
        //public int UserId { get; set; }


        public string? AccessToken { get; set; }
        public string? RefreshToken { get; set; }
        public int? UserId { get; set; }

        public string? Email { get; set; }

    }
}
