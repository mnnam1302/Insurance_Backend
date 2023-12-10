using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace backend.Models
{
    [Table("InsuranceOrder")]
    public class InsuranceOrder
    {
        [Column("Id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key] public int Id { get; set; }

        [Column("ContractId")]
        [Required]
        public int ContractId { get; set; }

        [Column("TotalCost")]
        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "Must be greater than or equal to 0")]
        public double TotalCost { get; set; }

        [Column("TotalPayment")]
        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "Must be greater than or equal to 0")]
        public double TotalPayment { get; set; }

        [Column("Descriptions")]
        [Required]
        public string Description { get; set; } = "";

        [Column("Status")]
        [Required]
        public string Status { get; set; } = "Processing";

        [Column("PaymentDate")]
        [Required]
        public DateTime? PaymentDate { get; set; }
    }
}
