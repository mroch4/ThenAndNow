namespace ThenAndNow.Constants
{
    public static class JsInteropKeys
    {
        private const string LeafletMapInterop = $"leafletMap{Interop}";
        public const string InitMap = $"{LeafletMapInterop}.init";

        private const string FirebaseInterop = $"firebase{Interop}";
        public const string GetVotesById = $"{FirebaseInterop}.getRatingById";
        public const string GetDetailsById = $"{FirebaseInterop}.getDetailsById";
        public const string UpdateRating = $"{FirebaseInterop}.updateRating";

        private const string LocalStorage = "localStorage";
        public const string GetItem = $"{LocalStorage}.getItem";
        public const string SetItem = $"{LocalStorage}.setItem";

        private const string Interop = "Interop";
    }
}
