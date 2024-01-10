using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace backend.DTO.Beneficiary
{
    public class CreateBeneficiaryDTO
    {
        [Required(ErrorMessage = "Email is required. You can enter the policyholer's email if beneficiary under 18 years old.")]
        [StringLength(255)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Fullname is required.")]
        [StringLength(255)]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Phone is required. You can enter the policyholer's phone if beneficiary under 18 years old.")]
        [StringLength(20)]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Sex is required.")]
        [StringLength(5)]
        public string Sex { get; set; }

        [Required(ErrorMessage = "Date of birth is required.")]
        public DateTime DateOfBirth { get; set; }

        [Required(ErrorMessage = "Card identification is required. You can enter the policyholer's card identification if beneficiary under 18 years old.")]
        [StringLength(20)]
        public string CardIdentification { get; set; }

        public IFormFile? ImageIdentification { get; set; }

        public string? ImageIdentificationUrl { get; set; }

        [Required(ErrorMessage = "Address is required.")]
        [StringLength(255)]
        public string Address { get; set; }

        [Required(ErrorMessage = "Relationship policyholder is required.")]
        [StringLength(100)]
        public string RelationshipPolicyholder { get; set; }

        public int UserId { get; set; }
    }
}
