using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace backend.DTO
{
    public class BeneficiaryDTO
    {
        public int BeneficiaryId { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [StringLength(255)]
        public string Email { get; set; } = "";

        [StringLength(255)]
        public string? FullName { get; set; }

        [Required(ErrorMessage = "Phone is required")]
        [StringLength(20)]
        public string Phone { get; set; } = "";

        [StringLength(5)]
        public string? Sex { get; set; }

        public DateTime? DateOfBirth { get; set; }

        [Required(ErrorMessage = "Card Identification is required")]
        [StringLength(20)]
        public string CardIdentification { get; set; } = "";

        public IFormFile? ImageIdentification { get; set; }
        public string? ImageUrl { get; set; }

        [StringLength(255)]
        public string? Address { get; set; }

        [Required(ErrorMessage = "Relationship policyholder is required")]
        [StringLength(100)]
        public string RelationshipPolicyholder { get; set; } = "";

        //Khỏi UserId -> Lấy từ context ra
        public int UserId { get; set; }
    }
}
