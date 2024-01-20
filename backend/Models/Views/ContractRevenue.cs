using Microsoft.EntityFrameworkCore;

namespace backend.Models.Views
{
    [Keyless]
    public class ContractRevenue
    {
        public int Year { get; set; }
        public int Month { get; set; }
        public decimal Revenue { get; set; }
    }
}
