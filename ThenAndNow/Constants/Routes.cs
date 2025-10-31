using ThenAndNow.Models;

namespace ThenAndNow.Constants
{
    public static class Routes
    {
        #region Internal

        public const string About = "/about";
        public const string Admin = "/admin";
        public const string Entry = "/entry";
        public const string Map = "/map";
        public const string Random = "/random";
        public const string Root = "/";
        public const string Tags = "/tags";

        #endregion

        #region External

        public const string Cyryl = "https://cyryl.poznan.pl/";
        public const string Flickr = "https://www.flickr.com/people/137860135@N05";
        public const string Fotopolska = "https://fotopolska.eu/";
        public const string Github = "https://github.com/mroch4";

        public const string GoogleMapBaseUrl = "http://maps.google.com/maps";

        #endregion

        #region Query Params

        public const string EntryIdQueryParamName = "id";
        public const string CurrentPageQueryParamName = "page";
        public const string PageSizeQueryParamName = "pageSize";
        public const string TagQueryParamName = "tag";
        public const string SortByQueryParamName = "sortBy";
        public const string SortingQueryParamName = "sorting";

        public static string SortByIdQueryParamName = nameof(SortBy.Id).ToLower();
        public static string SortByTitleQueryParamName = nameof(SortBy.Title).ToLower();
        public static string SortByDateNowQueryParamName = nameof(SortBy.DateNow).ToLower();

        public static string SortingAscQueryParamName = nameof(SortDirection.Ascending).ToLower();
        public static string SortingDescQueryParamName = nameof(SortDirection.Descending).ToLower();

        #endregion
    }
}
