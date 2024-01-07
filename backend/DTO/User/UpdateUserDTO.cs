using System.ComponentModel.DataAnnotations;

namespace backend.DTO.User
{
    public class UpdateUserDTO
    {
        public int UserId { get; set; }

        [MaxLength(255, ErrorMessage = "Full name cannot exceed 255 characters.")]
        public string FullName { get; set; }

        public string Phone { get; set; }

        public string Sex { get; set; }

        public DateTime DateOfBirth { get; set; }

        public string CardIdentification { get; set; }
    }
}
