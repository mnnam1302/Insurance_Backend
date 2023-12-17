using System.ComponentModel.DataAnnotations;

namespace backend.DTO
{
    public class GoogleTokenDTO
    {
        public string Email { get; set; }
        public string CredentialToken { get; set; }
    }
}
