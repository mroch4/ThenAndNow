using ThenAndNow.Models;
using ThenAndNow.Models.Database;

namespace ThenAndNow.Helpers
{
    public static class RequestHelper
    {
        public static Request GetDatabaseQuery(QueryParams queryParams)
        {
            var take = queryParams.PageSize.GetQueryTake();

            var query = new Request
            {
                Tag = queryParams.Tag,
                Skip = GetQueryOffset(queryParams.CurrentPage) * take,
                Take = take,
                SortBy = queryParams.SortBy,
                SortDirection = queryParams.SortDirection
            };

            return query;
        }

        public static int GetPagesCount(this int itemsCount, int pageSize)
        {
            return itemsCount > 0 ? (int)Math.Ceiling(itemsCount / (decimal)pageSize) : 0;
        }

        private static int GetQueryTake(this int pageSize)
        {
            return pageSize == 0 ? Constants.Constants.DefaultPageSize : pageSize;
        }

        private static int GetQueryOffset(int currentPage)
        {
            return currentPage <= 1 ? 0 : currentPage - 1;
        }
    }
}
