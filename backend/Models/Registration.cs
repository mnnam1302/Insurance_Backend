using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace backend.Models
{
    [Table("registrations")]
    public class Registration
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("registration_id")]
        public int RegistrationId { get; set; }

        [Column("registration_Date")]
        public DateTime RegistrationDate { get; set; } = DateTime.Now;

        [Required]
        [Column("start_Date")]
        public DateTime StartDate { get; set; }

        [Required]
        [Column("end_Date")]
        public DateTime EndDate { get; set; }

        [Column("basicInsuranceFee")]
        public decimal BasicInsuranceFee { get; set; }

        [Column("discount")]
        public decimal Discount { get; set; }

        [Column("totalSupplementalBenefitFee")]
        public decimal TotalSupplementalBenefitFee { get; set; }

        [Required]
        [StringLength(100)]
        [Column("registration_Status")]
        public string RegistrationStatus { get; set; }

        [Column("beneficiary_id")]
        public int BeneficiaryId { get; set; }

        [Column("insurance_id")]
        public int InsuranceId { get; set; }

        [ForeignKey("BeneficiaryId")]
        public Beneficiary Beneficiary { get; set; }

        [ForeignKey("InsuranceId")]
        public Insurance Insurance { get; set; }
    }
}
