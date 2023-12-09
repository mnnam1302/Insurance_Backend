using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace backend.DTO
{
    public class RegistrationDTO
    {
        public int RegistrationId { get; set; }
        public DateTime RegistrationDate { get; set; }

        [Required(ErrorMessage = "Start Date is required")]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "End Date is required")]
        public DateTime EndDate { get; set; }

        public decimal BasicInsuranceFee { get; set; }

        public decimal Discount { get; set; } = 0;

        public decimal TotalSupplementalBenefitFee { get; set; }

        public string? RegistrationStatus { get; set; }

        public int BeneficiaryId { get; set; }

        public int InsuranceId { get; set; }
    }
}
