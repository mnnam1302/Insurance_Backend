using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace backend.DTO
{
    public class PaymentRequestDTO
    {
        public int paymentrequest_id { get; set; }

        [Column("total_cost")]
        [Range(0, double.MaxValue, ErrorMessage = "Must be greater than or equal to 0")]
        public double total_cost { get; set; }

        [Column("total_payment")]
        [Range(0, double.MaxValue, ErrorMessage = "Must be greater than or equal to 0")]
        public double total_payment { get; set; }

        [Column("Description")]
        [Required]
        public string? Description { get; set; }

        public IFormFile? ImageIdentification { get; set; }

        public string? image_identification_url { get; set; }

        [Column("request_status")]
        public string Status { get; set; } = "in process";

        [Column("beneficiary_id")]
        [Required]
        public int Beneficiaries_id { get; set; }

        public DateTime? update_date { get; set; } = DateTime.Now;
    }
}
