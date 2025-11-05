using ThenAndNow.Enums;
using ThenAndNow.Helpers;

namespace ThenAndNow.Models
{
    public class QueryParams
    {
        public QueryParams()
        {
            CurrentPage = 0;
            PageSize = Constants.Constants.DefaultPageSize;
            Tag = null;
        }

        public QueryParams(string currentPage, string pageSize, Sorting sorting, string tag)
        {
            CurrentPage = currentPage.GetCurrentPage();
            PageSize = pageSize.GetPageSize();
            Sorting = sorting;
            Tag = tag;
        }

        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public Sorting Sorting { get; set; }
        public SortBy SortBy => Sorting.ToSortBy();
        public SortDirection SortDirection => Sorting.ToSortDirection();
        public string Tag { get; set; }
    }
}