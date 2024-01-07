using System.ComponentModel.DataAnnotations;

namespace backend.DTO.Auth
{
    public class GoogleTokenDTO
    {
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email addrress")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Credential token is required")]
        public string CredentialToken { get; set; }
    }
}
