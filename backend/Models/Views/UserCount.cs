using Microsoft.EntityFrameworkCore;

namespace backend.Models.Views
{
    [Keyless]
    public class UserCount
    {
        public int Year { get; set; }
        public int Month { get; set; }
        public int Total { get; set; }
    }
}
