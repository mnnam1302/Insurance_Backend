using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models
{
    [Table("benefit_types")]
    public class BenefitType
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("BenefitType_ID")]
        public int BenefitTypeId { get; set; }

        [Required]
        [Column("NameBenefitType")]
        [StringLength(255)]
        public string NameBenefitType { get; set; } = "";
    }
}
