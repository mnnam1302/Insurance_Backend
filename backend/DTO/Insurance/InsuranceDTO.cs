using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace backend.DTO.Insurance
{
    public class InsuranceDTO
    {
        public int InsuranceId { get; set; }

        public string NameInsurance { get; set; }

        public int FromAge { get; set; }

        public int ToAge { get; set; }

        public double Price { get; set; }

        public double Discount { get; set; }

        public double PriceDiscount { get; set; }

        public string Status { get; set; }

        public int InsuranceTypeId { get; set; }
    }
}
