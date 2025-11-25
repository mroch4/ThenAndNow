namespace ThenAndNow.Constants
{
    public static class JsInteropKeys
    {
        #region Blazor Interop

        public const string ScrollTo = $"{BlazorInterop}.scrollTo";
        public const string ScrollTop = $"{BlazorInterop}.scrollTop";

        #endregion

        #region Bootstrap Interop

        public const string ShowModal = $"{BootstrapInterop}.showModal";

        #endregion

        #region Leaflet Map Interop

        public const string InitMap = $"{LeafletMapInterop}.init";

        #endregion

        #region Local Storage

        public const string GetItem = $"{LocalStorage}.getItem";
        public const string SetItem = $"{LocalStorage}.setItem";

        private const string LocalStorage = "localStorage";

        #endregion

        #region Rating

        public const string GetRatingById = $"{FirebaseInterop}.getRatingById";
        public const string UpdateRating = $"{FirebaseInterop}.updateRating";

        #endregion

        #region Replies

        public const string AddReply = $"{FirebaseInterop}.addReply";
        public const string GetRepliesById = $"{FirebaseInterop}.getRepliesById";

        #endregion

        private const string BlazorInterop = $"blazor{Interop}";
        private const string BootstrapInterop = $"bootstrap{Interop}";
        private const string FirebaseInterop = $"firebase{Interop}";
        private const string LeafletMapInterop = $"leafletMap{Interop}";

        private const string Interop = "Interop";
    }
}
