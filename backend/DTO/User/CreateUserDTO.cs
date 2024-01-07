using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace backend.DTO.User
{
    public class CreateUserDTO
    {
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        [MaxLength(255, ErrorMessage = "Email cannot exceed 255 characters")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }

        public string FullName { get; set; }

        [Required(ErrorMessage = "You must enter your phone")]
        public string Phone { get; set; }

        public string Sex { get; set; }

        public DateTime? DateOfBirth { get; set; }

        [Required(ErrorMessage = "You must enter your card identification")]
        public string CardIdentification { get; set; }
    }
}
