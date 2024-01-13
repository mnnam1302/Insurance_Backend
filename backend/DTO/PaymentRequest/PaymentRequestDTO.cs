using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace backend.DTO
{
    public class PaymentRequestDTO
    {
        public int PaymentRequestId { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Must be greater than or equal to 0")]
        public double TotalCost { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Must be greater than or equal to 0")]
        public double TotalPayment { get; set; }

        [Required(ErrorMessage = "Please enter your description")]
        public string? Description { get; set; }

        public string? ImagePaymentRequestUrl { get; set; }

        public string RequestStatus { get; set; } = string.Empty;

        public int ContractId { get; set; }

        public DateTime? UpdatedDate { get; set; }
    }
}
