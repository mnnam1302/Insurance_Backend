using System.ComponentModel.DataAnnotations;

namespace backend.DTO.PaymentRequest
{
    public class CreatePaymentRequestDTO
    {
        [Range(0, double.MaxValue, ErrorMessage = "Must be greater than or equal to 0")]
        public decimal TotalCost { get; set; }

        public string? Description { get; set; }

        public IFormFile? ImagePaymentRequest { get; set; }

        public string ImagePaymentRequestUrl { get; set; } = string.Empty;

        public int ContractId { get; set; }
    }
}