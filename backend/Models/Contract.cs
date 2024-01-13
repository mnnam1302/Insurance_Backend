using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace backend.Models
{
    [Table("contracts")]
    public class Contract
    {
        [Column("contract_id")]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ContractId { get; set; }

        [Column("insurance_Code")]
        public string InsuranceCode { get; set; } = string.Empty;

        [Column("signing_Date")]
        public DateTime? SigningDate { get; set; }

        [Column("start_Date")]
        public DateTime? StartDate { get; set; }

        [Column("end_Date")]
        public DateTime? EndDate { get; set; }

        [Column("total_Turn")]
        public int TotalTurn { get; set; }

        [Column("contract_Status")]
        public string ContractStatus { get; set; } = string.Empty;

        [Column("initial_Fee")]
        public decimal InitialFeePerTurn { get; set; }

        [Column("bonus_Fee")]
        public decimal BonusFee { get; set; }

        [Column("discount")]
        public decimal Discount { get; set; }

        [Column("periodic_Fee")]
        public decimal PeriodFee { get; set; }

        [Column("total_Fee")]
        public decimal TotalFee { get; set; }

        [Column("user_id")]
        public int UserId { get; set; }

        [Column("beneficiary_id")]
        public int BeneficiaryId { get; set; }

        [Column("registration_id")]
        public int RegistrationId { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; }

        [ForeignKey("BeneficiaryId")]
        public Beneficiary Beneficiary { get; set; }

        [ForeignKey("RegistrationId")]
        public Registration Registration { get; set; }
    }
}
