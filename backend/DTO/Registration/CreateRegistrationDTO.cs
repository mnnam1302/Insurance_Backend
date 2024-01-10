using System.ComponentModel.DataAnnotations;

namespace backend.DTO.Registration
{
    public class CreateRegistrationDTO
    {
        [Required(ErrorMessage = "Start date is required.")]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "End date is required.")]
        public DateTime EndDate { get; set; }

        public decimal BasicInsuranceFee { get; set; }

        public decimal Discount { get; set; } = 0;

        public decimal TotalSupplementalBenefitFee { get; set; }

        [Required]
        [StringLength(100)]
        public string RegistrationStatus { get; set; }

        public int BeneficiaryId { get; set; }

        public int InsuranceId { get; set; }
    }
}
