namespace backend.DTO.Vnpay
{
    public class VnPayRequestModel
    {
        public string FullName { get; set; } = string.Empty;

        public int PaymentContractId { get; set; }
        public int ContractId { get; set; }
        public string Description { get; set; } = string.Empty;
        public int Amount { get; set; }
        public DateTime CreateDate { get; set; } = DateTime.Now;
    }
}
