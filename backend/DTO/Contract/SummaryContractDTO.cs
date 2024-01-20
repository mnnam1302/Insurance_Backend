using Microsoft.AspNetCore.Routing.Constraints;

namespace backend.DTO.Contract
{
    public class SummaryContractDTO
    {
        public decimal RevenueCurrentMonth { get; set; }
        public decimal RateRevenueCurrentMonth { get; set; }
    }
}
