using System.ComponentModel.DataAnnotations;

namespace backend.DTO
{
    public class UpdateInsuranceOrder
    {
        [Required(ErrorMessage = "You must enter a value")]
        [Range(0, double.MaxValue, ErrorMessage = "Must be greater than or equal to 0")]
        public double TotalPayment { get; set; }

        [Required(ErrorMessage = "Please choose a status below")]
        public string Status { get; set; } = "";

        public DateTime? PaymentDate { get; set; } = DateTime.Now;
    }
}
