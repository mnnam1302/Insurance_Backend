namespace backend.DTO.PaymentRequest
{
    public class UpdatePaymentRequestDTO
    {
        public decimal Payment {  get; set; }


        public string Status { get; set; } = string.Empty;

    }
}