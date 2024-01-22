
namespace backend.DTO.Beneficiary
{
    public class BeneficiaryDTO
    {
        public int BeneficiaryId { get; set; }
        public string Email { get; set; }

        public string FullName { get; set; }

        public string Phone { get; set; }

        public string Sex { get; set; }

        public DateTime DateOfBirth { get; set; }

        public string CardIdentification { get; set; }
        public string? ImageIdentificationUrl { get; set; }

        public string Address { get; set; }

        public string RelationshipPolicyholder { get; set; }

        public int UserId { get; set; }

        public DateTime? CreatedDate { get; set; }
    }
}