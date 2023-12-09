using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace backend.DTO
{
    public class CreateInsuranceOrder
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required(ErrorMessage = "You must enter your contract id")]
        public int ContractId { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Must be greater than or equal to 0")]
        public double TotalCost { get; set; }

        [Range(0, double.MaxValue)]
        public double TotalPayment { get; set; }

        [Required(ErrorMessage = "Please provide your medical information")]
        public string Description { get; set; } = "";

        public string Status { get; set; } = "Processing";

        public DateTime? PaymentDate { get; set; } = null;
    }
}
