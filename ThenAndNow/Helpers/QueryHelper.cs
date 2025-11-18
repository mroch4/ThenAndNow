using ThenAndNow.Constants;
using ThenAndNow.Enums;
using ThenAndNow.Models;

namespace ThenAndNow.Helpers
{
    public static class QueryHelper
    {
        public static int GetCurrentPage(this string value)
        {
            return int.TryParse(value, out var currentPage) && currentPage >= 0 ? currentPage : 0;
        }

        public static int GetPageSize(this string value)
        {
            return int.TryParse(value, out var pageSize) && pageSize >= 0 ? pageSize : Constants.Constants.DefaultPageSize;
        }

        public static string ToQueryString(this QueryParams queryParams)
        {
            var queryString = $"{Routes.Root}";

            var queryMembers = new List<string>();

            if (queryParams.CurrentPage > 0)
            {
                queryMembers.Add($"{Routes.CurrentPageQueryParamName}={queryParams.CurrentPage}");
            }

            if (!string.IsNullOrEmpty(queryParams.Tag))
            {
                queryMembers.Add($"{Routes.TagQueryParamName}={queryParams.Tag}");
            }

            if (ShouldIncludePageSize(queryParams.PageSize))
            {
                queryMembers.Add($"{Routes.PageSizeQueryParamName}={queryParams.PageSize}");
            }

            if (ShouldIncludeSorting(queryParams.SortBy, queryParams.SortDirection))
            {
                queryMembers.Add($"{Routes.SortByQueryParamName}={queryParams.SortBy.ToString().ToLower()}");
                queryMembers.Add($"{Routes.SortingQueryParamName}={queryParams.SortDirection.ToString().ToLower()}");
            }

            if (queryMembers.Any())
            {
                queryString += $"?{string.Join("&", queryMembers)}";
            }

            return queryString;
        }

        #region Private Methods

        private static bool ShouldIncludePageSize(int pageSize)
        {
            return pageSize > 0 &&
                   pageSize != Constants.Constants.DefaultPageSize;
        }

        private static bool ShouldIncludeSorting(SortBy sortBy, SortDirection sortDirection)
        {
            return !(sortBy == SortBy.Id && sortDirection == SortDirection.Desc);
        }

        #endregion
    }
}