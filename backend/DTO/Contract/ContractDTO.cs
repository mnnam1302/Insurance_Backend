using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace backend.DTO.Contract
{
    public class ContractDTO
    {
        public int ContractId { get; set; }
        public string InsuranceCode { get; set; } = string.Empty;
        public DateTime? SigningDate { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int TotalTurn { get; set; }
        public string ContractStatus { get; set; } = string.Empty;
        public decimal InitialFeePerTurn { get; set; }
        public decimal BonusFee { get; set; }
        public decimal Discount { get; set; }
        public decimal PeriodFee { get; set; }
        public decimal TotalFee { get; set; }
        public int UserId { get; set; }
        public int BeneficiaryId { get; set; }
        public int RegistrationId { get; set; }
    }
}
