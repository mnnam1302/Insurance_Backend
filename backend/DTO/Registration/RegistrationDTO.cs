using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace backend.DTO.Registration
{
    public class RegistrationDTO
    {
        public int RegistrationId { get; set; }

        public DateTime RegistrationDate { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public decimal BasicInsuranceFee { get; set; }

        public decimal Discount { get; set; }

        public decimal TotalSupplementalBenefitFee { get; set; }

        public string RegistrationStatus { get; set; }

        public int BeneficiaryId { get; set; }

        public int InsuranceId { get; set; }
    }
}
