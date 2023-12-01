using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace backend.Models
{
    [Table("insurance_types")]
    public class InsuranceType
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("InsuranceType_ID")]
        public int InsuranceTypeId { get; set; }

        [Required(ErrorMessage ="Name insurance type is required")]
        [Column("NameInsuranceType")]
        [StringLength(255)]
        public string NameInsuranceType { get; set; } = "";
    }
}
