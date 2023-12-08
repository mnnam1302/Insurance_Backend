using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace backend.Models
{
    [Table("insurances")]
    public class Insurance
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("Insurance_ID")]
        public int InsuranceId { get; set; }

        [Required(ErrorMessage = "Name insurance is required")]
        [Column("NameInsurance")]
        [StringLength(255)]
        public string NameInsurance { get; set; } = "";

        [Required]
        [Column("FromAge")]
        public int FromAge { get; set; }

        [Required]
        [Column("ToAge")]
        public int ToAge { get; set; }

        [Column("Price")]
        [Range(0, double.MaxValue, ErrorMessage = "Price must be greater than or equal to 0")]
        public double Price { get; set; }

        [Column("Discount")]
        [Range(0, double.MaxValue, ErrorMessage = "Discount must be greater than or equal to 0")]
        public double Discount { get; set; } = 0;

        [Column("Status")]
        [StringLength(40)]
        public string Status { get; set; } = "Active";

        [Column("InsuranceType_ID")]
        public int InsuranceTypeId { get; set; }

        [ForeignKey("InsuranceTypeId")]
        public InsuranceType? InsuranceType { get; set; }
    }
}
