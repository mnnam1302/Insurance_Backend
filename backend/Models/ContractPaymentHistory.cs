using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace backend.Models
{
    [Table("contract_payment_histories")]
    public class ContractPaymentHistory
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("paymentcontract_Id")]
        public int PaymentContractId { get; set; }

        [Column("transaction_Code")]
        [MaxLength(25)]
        public string TransactionCode { get; set; } = string.Empty;

        [Column("payment_Date")]
        public DateTime? PaymentDate { get; set; } = DateTime.Now;

        [Column("payment_Amount")]
        public decimal PaymentAmount { get; set; }

        [Column("service_Payment")]
        [MaxLength(20)]
        public string ServicePayment { get; set; } = string.Empty;

        [Column("bank_Name")]
        [MaxLength(50)]
        public string BankName { get; set; } = string.Empty;

        [Column("status")]
        [MaxLength(30)]
        public string Status { get; set; } = "Chờ thanh toán";

        [Column("contract_Id")]
        public int? ContractId { get; set; }

        [ForeignKey("ContractId")]
        public Contract Contract { get; set; }
    }
}
