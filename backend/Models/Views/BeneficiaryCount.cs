using Microsoft.EntityFrameworkCore;

namespace backend.Models.Views
{
    [Keyless]
    public class BeneficiaryCount
    {
        public string Label { get; set; }
        public int Total { get; set; }
    }
}
