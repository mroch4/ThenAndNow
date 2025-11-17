namespace ThenAndNow.Constants
{
    public static class JsInteropKeys
    {
        #region Auth

        public const string GetCurrentUser = $"{FirebaseInterop}.getCurrentUser";
        public const string SignInWithGoogle = $"{FirebaseInterop}.signInWithGoogle";
        public const string SignOut = $"{FirebaseInterop}.signOut";

        #endregion

        #region Bootstrap

        public const string ShowModal = $"{BootstrapInterop}.showModal";

        private const string BootstrapInterop = $"bootstrap{Interop}";

        #endregion

        #region Leaflet Map

        public const string InitMap = $"{LeafletMapInterop}.init";

        private const string LeafletMapInterop = $"leafletMap{Interop}";

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

        public const string ScrollTo = $"window.blazor{Interop}.scrollTo";
        public const string ScrollTop = $"window.blazor{Interop}.scrollTop";

        private const string FirebaseInterop = $"firebase{Interop}";

        private const string Interop = "Interop";
    }
}
