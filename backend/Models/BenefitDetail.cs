using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models
{
    [Table("benefit_details")]
    public class BenefitDetail
    {
        [Column("Insurance_ID")]
        public int InsuranceId { get; set; }

        [Column("Benefit_ID")]
        public int BenefitId { get; set; }

        [Column("ClaimSettlementFee")]
        public int ClaimSettlementFee { get; set; }

        [ForeignKey("BenefitId")]
        public Benefit? Benefit { get; set; }

        [ForeignKey("InsuranceId")]
        public Insurance? Insurance { get; set; }
    }
}
