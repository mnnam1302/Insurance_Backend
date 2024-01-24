using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace backend.Models
{
    [Table("payment_request")]
    public class PaymentRequest
    {
        [Key] 
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("paymentrequest_id")]
        public int PaymentRequestId { get; set; }

        [Required]
        [Column("total_cost")]
        public decimal TotalCost { get; set; }

        [Column("total_payment")]
        public decimal TotalPayment { get; set; }

        [Column("Description")]
        public string Description { get; set; } = string.Empty;

        [Column("image_identification_url")]
        public string? ImagePaymentRequestUrl { get; set; }

        [Column("request_status")]
        public string RequestStatus { get; set; } = string.Empty;

        [Column("contract_id")]
        public int ContractId { get; set; }

        [Column("update_date")]
        public DateTime? UpdatedDate { get; set; } = DateTime.Now;

        [ForeignKey("ContractId")]
        public Contract? Contract { get; set; }
    }
}
