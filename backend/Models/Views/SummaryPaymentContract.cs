using Microsoft.EntityFrameworkCore;

namespace backend.Models.Views
{
    [Keyless]
    public class SummaryPaymentContract
    {
        public int Year { get; set; }
        public int Month { get; set; }
        public decimal Amount { get; set; }
    }
}
