using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace backend.DTO
{
    public class InsuranceOrderDTO
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key] public int Id { get; set; }

        public int ContractId { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Price must be greater than or equal to 0")]
        public double TotalCost { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Price must be greater than or equal to 0")]
        public double TotalPayment { get; set; }

        public string Description { get; set; } = "";

        public string Status { get; set; } = "Processing";

        public DateTime? PaymentDate { get; set; }
    }
}
