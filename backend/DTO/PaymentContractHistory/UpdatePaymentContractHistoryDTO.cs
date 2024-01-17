namespace backend.DTO.PaymentContractHistory
{
    public class UpdatePaymentContractHistoryDTO
    {
        public int PaymentContractId { get; set; }
        public string TransactionCode { get; set; }
        public string ServicePayment { get; set; }
        public string BankName { get; set; }
        public string Status { get; set; }
    }
}
