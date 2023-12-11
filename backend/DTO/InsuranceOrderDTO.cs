using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace backend.DTO
{
    public class InsuranceOrderDTO
    {
        [Column("id")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Countract id is required")]
        public int ContractId { get; set; }

        [Required(ErrorMessage = "total cost must be entered")]
        [Range(0, double.MaxValue, ErrorMessage = "Price must be greater than or equal to 0")]
        public double TotalCost { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Price must be greater than or equal to 0")]
        public double TotalPayment { get; set; } = 0;

        [Required(ErrorMessage = "Description is required")]
        [StringLength(255)]
        public string Description { get; set; } = "";

        [StringLength(255)]
        public string Status { get; set; } = "Processing";

        public DateTime? PaymentDate { get; set; } = DateTime.Now;
    }
}
