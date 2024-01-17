namespace backend.DTO.Vnpay
{
    public class VnPaymentResponseModel
    {
        public int PaymentContractId { get; set; }
        public bool Success { get; set; }
        public string PaymentMethod { get; set; }
        public string BankCode { get; set; }
        public string BankTransactioNo { get; set; }
        public string OrderDescription { get; set; }
        public string Token { get; set; }
        public string VnPayResponseCode { get; set; }
    }
}
