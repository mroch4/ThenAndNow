using ThenAndNow.Constants;
using ThenAndNow.Enums;

namespace ThenAndNow.Helpers
{
    public static class SortingExtensions
    {
        public static SortBy ToSortBy(this Sorting value)
        {
            if (value is Sorting.DateNowDescending or Sorting.DateNowAscending) return SortBy.DateNow;

            return value is Sorting.TitleDescending or Sorting.TitleAscending
                ? SortBy.Title
                : SortBy.Id;
        }

        public static SortDirection ToSortDirection(this Sorting value)
        {
            return value is Sorting.DateNowAscending or Sorting.TitleAscending or Sorting.IdAscending
                ? SortDirection.Asc
                : SortDirection.Desc;
        }

        public static Sorting ToSorting(string sortBy, string sortDirection)
        {
            if (string.Equals(sortBy, Routes.SortByDateNowQueryParamName, StringComparison.OrdinalIgnoreCase) &&
                string.Equals(sortDirection, Routes.SortingDescQueryParamName, StringComparison.OrdinalIgnoreCase))
            {
                return Sorting.DateNowDescending;
            }

            if (string.Equals(sortBy, Routes.SortByDateNowQueryParamName, StringComparison.OrdinalIgnoreCase) &&
                string.Equals(sortDirection, Routes.SortingAscQueryParamName, StringComparison.OrdinalIgnoreCase))
            {
                return Sorting.DateNowAscending;
            }

            if (string.Equals(sortBy, Routes.SortByTitleQueryParamName, StringComparison.OrdinalIgnoreCase) &&
                string.Equals(sortDirection, Routes.SortingDescQueryParamName, StringComparison.OrdinalIgnoreCase))
            {
                return Sorting.TitleDescending;
            }

            if (string.Equals(sortBy, Routes.SortByTitleQueryParamName, StringComparison.OrdinalIgnoreCase) &&
                string.Equals(sortDirection, Routes.SortingAscQueryParamName, StringComparison.OrdinalIgnoreCase))
            {
                return Sorting.TitleAscending;
            }

            if (string.Equals(sortBy, Routes.SortByIdQueryParamName, StringComparison.OrdinalIgnoreCase) &&
                string.Equals(sortDirection, Routes.SortingAscQueryParamName, StringComparison.OrdinalIgnoreCase))
            {
                return Sorting.IdAscending;
            }

            return Sorting.IdDescending;
        }
    }
}