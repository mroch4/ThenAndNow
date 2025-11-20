namespace ThenAndNow.Models
{
    public class User
    {
        #region User Info

        public string Name { get; set; }
        public string Email { get; set; }
        public string Color { get; set; }

        public bool IsAuthenticated =>
            !string.IsNullOrEmpty(Name) &&
            !string.IsNullOrEmpty(Email);

        #endregion

        #region Preferences

        public bool ClosedBanner { get; set; }
        public bool OriginalPhotoFirst { get; set; }

        #endregion

        public int[] Votes { get; set; }
    }
}
