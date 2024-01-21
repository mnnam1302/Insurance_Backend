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
        [Range(0, double.MaxValue, ErrorMessage = "Must be greater than or equal to 0")]
        [Column("total_cost")]
        public decimal TotalCost { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Must be greater than or equal to 0")]
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
        public DateTime? UpdatedDate { get; set; }

        [ForeignKey("ContractId")]
        public Contract? Contract { get; set; }
    }
}
