namespace ThenAndNow.Constants
{
    public static class JsInteropKeys
    {
        #region Leaflet Map

        public const string InitMap = $"{LeafletMapInterop}.init";

        private const string LeafletMapInterop = $"leafletMap{Interop}";

        #endregion

        #region Auth

        public const string GetCurrentUser = $"{FirebaseInterop}.getCurrentUser";
        public const string SignInWithGoogle = $"{FirebaseInterop}.signInWithGoogle";
        public const string SignOut = $"{FirebaseInterop}.signOut";

        #endregion

        #region Rating

        public const string GetRatingById = $"{FirebaseInterop}.getRatingById";
        public const string UpdateRating = $"{FirebaseInterop}.updateRating";

        #endregion

        #region Replies

        public const string AddReply = $"{FirebaseInterop}.addReply";
        public const string GetRepliesById = $"{FirebaseInterop}.getRepliesById";

        #endregion

        #region Local Storage

        public const string GetItem = $"{LocalStorage}.getItem";
        public const string SetItem = $"{LocalStorage}.setItem";

        private const string LocalStorage = "localStorage";

        #endregion

        public const string ScrollTop = "window.scrollTo";

        private const string FirebaseInterop = $"firebase{Interop}";

        private const string Interop = "Interop";
    }
}
