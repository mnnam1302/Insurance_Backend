namespace backend.Responses
{
    public class BasePagingQuery
    {
        public int? PageSize { get; set; } = 5;
        public int? PageIndex { get; set; } = 1;
    }
}
