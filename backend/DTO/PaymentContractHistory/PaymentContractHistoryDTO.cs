using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace backend.DTO.PaymentContractHistory
{
    public class PaymentContractHistoryDTO
    {
        public int PaymentContractId { get; set; }

        public string TransactionCode { get; set; }

        public DateTime? PaymentDate { get; set; }

        public decimal PaymentAmount { get; set; }

        public string ServicePayment { get; set; }

        public string BankName { get; set; }

        public string Status { get; set; }

        public int? ContractId { get; set; }
    }
}
