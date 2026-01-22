namespace Profiles.DAL.Models
{
    public class PaginationParameters
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int Skip => (PageNumber - 1) * PageSize;
    }
}
