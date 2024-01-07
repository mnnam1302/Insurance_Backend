using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace backend.Models
{
    [Table("users")]
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("user_id")]
        public int UserId { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        [MaxLength(255, ErrorMessage = "Email cannot exceed 255 characters.")]
        [Column("email")]
        public string Email { get; set; } = "";

        [Required(ErrorMessage = "Hashed password is required.")]
        [MaxLength(200, ErrorMessage = "Hashed password cannot exceed 200 characters.")]
        [Column("hashed_password")]
        public string Password { get; set; } = "";

        [MaxLength(255, ErrorMessage = "Full name cannot exceed 255 characters.")]
        [Column("full_name")]
        public string FullName { get; set; } = "";

        [Required(ErrorMessage = "Phone number is required.")]
        [MaxLength(20, ErrorMessage = "Phone number cannot exceed 20 characters.")]
        [RegularExpression(@"^\+?\d{10,15}$", ErrorMessage = "Invalid phone number")]
        [Column("phone")]
        public string Phone { get; set; } = "";

        [Column("sex")]
        [MaxLength(5, ErrorMessage ="Sex cannot exceed 5 charaters")]
        public string? Sex { get; set; }


        [Column("date_of_birth")]
        public DateTime DateOfBirth { get; set; }

        [Required(ErrorMessage = "Card identification is required")]
        [MaxLength(20, ErrorMessage = "Card identification cannot exceed 00 characters.")]
        [Column("card_identification")]
        public string CardIdentification { get; set; }
    }
}
