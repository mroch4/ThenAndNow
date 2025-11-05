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
            if (sortBy == Routes.SortByDateNowQueryParamName &&
                sortDirection == Routes.SortingDescQueryParamName)
            {
                return Sorting.DateNowDescending;
            }

            if (sortBy == Routes.SortByDateNowQueryParamName &&
                sortDirection == Routes.SortingAscQueryParamName)
            {
                return Sorting.DateNowAscending;
            }

            if (sortBy == Routes.SortByTitleQueryParamName &&
                sortDirection == Routes.SortingDescQueryParamName)
            {
                return Sorting.TitleDescending;
            }

            if (sortBy == Routes.SortByTitleQueryParamName &&
                sortDirection == Routes.SortingAscQueryParamName)
            {
                return Sorting.TitleAscending;
            }

            if (sortBy == Routes.SortByIdQueryParamName &&
                sortDirection == Routes.SortingAscQueryParamName)
            {
                return Sorting.IdAscending;
            }

            return Sorting.IdDescending;
        }
    }
}