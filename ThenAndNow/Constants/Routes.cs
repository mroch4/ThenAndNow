using ThenAndNow.Enums;

namespace ThenAndNow.Constants
{
    public static class Routes
    {
        #region Internal

        public const string Contact = "/contact";
        public const string Entry = "/entry";
        public const string Map = "/map";
        public const string Random = "/random";
        public const string Root = "/";
        public const string Tags = "/tags";

        #endregion

        #region External

        public const string Cyryl = "https://cyryl.poznan.pl/";
        public const string Fotopolska = "https://poznan.fotopolska.eu/";
        public const string GoogleMapBaseUrl = "http://maps.google.com/maps";

        #endregion

        #region Socials

        public const string Facebook = "https://www.facebook.com/sharer/sharer.php?u=";
        public const string Flickr = "https://www.flickr.com/people/137860135@N05";
        public const string GitHub = "https://github.com/mroch4";
        public const string LinkedIn = "https://www.linkedin.com/shareArticle?mini=true&url=";
        public const string MailTo = "mailto:?body=";
        public const string MailToAuthor = $"mailto:{Constants.AuthorEmail}?body=";
        public const string Messenger = "fb-messenger://share/?link=";
        public const string Sms = "sms:?body=";
        public const string WhatsUp = "https://api.whatsapp.com/send?text=";
        public const string XTwitter = "https://twitter.com/intent/tweet?url=";

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

        public static string SortingAscQueryParamName = nameof(SortDirection.Asc).ToLower();
        public static string SortingDescQueryParamName = nameof(SortDirection.Desc).ToLower();

        #endregion
    }
}
