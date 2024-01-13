namespace backend.DTO.PaymentRequest
{
    public class UpdatePaymentRequestDTO
    {
        public double Payment {  get; set; }

        public string Status { get; set; } = string.Empty;
    }
}