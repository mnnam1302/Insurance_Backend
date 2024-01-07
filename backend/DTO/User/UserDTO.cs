using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace backend.DTO.User
{
    public class UserDTO
    {
        public int UserId { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        [MaxLength(255, ErrorMessage = "Email cannot exceed 255 characters.")]
        public string Email { get; set; }

        [MaxLength(255, ErrorMessage = "Full name cannot exceed 255 characters.")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Phone number is required.")]
        [MaxLength(20, ErrorMessage = "Phone number cannot exceed 20 characters.")]
        [RegularExpression(@"^\+?\d{10,15}$", ErrorMessage = "Invalid phone number")]
        public string Phone { get; set; }

        [Column("sex")]
        [MaxLength(5, ErrorMessage = "Sex cannot exceed 5 charaters")]
        public string? Sex { get; set; }

        public DateTime? DateOfBirth { get; set; }

        [Required(ErrorMessage = "Card identification is required")]
        [MaxLength(20, ErrorMessage = "Card identification cannot exceed 00 characters.")]
        public string CardIdentification { get; set; }
    }
}
