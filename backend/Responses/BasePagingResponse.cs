using System.Runtime.CompilerServices;

namespace backend.Responses
{
    public class BasePagingResponse<T> where T : class
    {
        public List<T> Data { get; set; }   // Dữ liệu
        public int TotalItemSelected { get; set; } // Tổng số record được chọn
        public int TotalItems { get; set; } // Tổng số record trong db
        public int PageSize { get; set; }   // Page size
        public int CurrentPage { get; set; }  // Current page
        public int TotalPages { get; set; }  // Total page
        public bool PreviousPage => CurrentPage > 1;
        public bool NextPage => CurrentPage < TotalPages;
    }
}
