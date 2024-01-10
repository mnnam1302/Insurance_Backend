using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.DTO.Registration
{
    public class CreateRegistrationDTO
    {
        [Required(ErrorMessage = "Start date is required.")]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "End date is required.")]
        public DateTime EndDate { get; set; }

        [Required(ErrorMessage = "Basic insurance fee is required.")]
        public decimal BasicInsuranceFee { get; set; }

        [Required(ErrorMessage = "Discount is required.")]
        public decimal Discount { get; set; }

        public decimal TotalSupplementalBenefitFee { get; set; }

        [Required(ErrorMessage = "Beneficiary id is required.")]
        public int BeneficiaryId { get; set; }

        [Required(ErrorMessage = "Insurance id is required.")]
        public int InsuranceId { get; set; }
    }
}
