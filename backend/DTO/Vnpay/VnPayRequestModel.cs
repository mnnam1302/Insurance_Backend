namespace backend.DTO.Vnpay
{
    public class VnPayRequestModel
    {
        public string FullName { get; set; } = string.Empty;
        public int ContractId { get; set; }
        public string Description { get; set; } = "Thanh toán cho hợp đồng: "; // tạm thời
        public double Amount { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
