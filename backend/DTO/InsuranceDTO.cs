using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace backend.DTO
{
    public class InsuranceDTO
    {
        public int InsuranceId { get; set; }

        public string NameInsurance { get; set; } = "";

        public int FromAge { get; set; }

        public int ToAge { get; set; }

        public double Price { get; set; }

        public double Discount { get; set; } = 0;

        public double PriceDiscount { get; set; } = 0;
 
        public string Status { get; set; } = "Active";

        public int InsuranceTypeId { get; set; }
    }
}
