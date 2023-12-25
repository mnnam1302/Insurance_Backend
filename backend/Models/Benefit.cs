using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace backend.Models
{
    [Table("benefits")]
    public class Benefit
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("Benefit_ID")]
        public int BenefitId { get; set; }

        [Required]
        [Column("NameBenefit")]
        [StringLength(255)]
        public string NameBenefit { get; set; } = "";

        [Column("Period")]
        [StringLength(50)]
        public string Period { get; set; } = "";

        [Column("BenefitType_ID")]
        public int BenefitTypeId { get; set; }

        [ForeignKey("BenefitTypeId")]
        public BenefitType? BenefitType { get; set; }
    }
}
