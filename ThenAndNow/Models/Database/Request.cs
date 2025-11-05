using ThenAndNow.Enums;

namespace ThenAndNow.Models.Database
{
    public class Request
    {
        public string Tag { get; set; }
        public int Take { get; set; }
        public int Skip { get; set; }
        public SortBy SortBy { get; set; }
        public SortDirection SortDirection { get; set; }
    }
}
