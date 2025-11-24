namespace ThenAndNow.Models
{
    public class User
    {
        #region User Info

        public string Name { get; set; }
        public string Email { get; set; }
        public string Icon { get; set; }

        public bool IsAuthenticated =>
            !string.IsNullOrEmpty(Name) &&
            !string.IsNullOrEmpty(Email);

        #endregion

        public Preferences Preferences { get; set; } = new();
        public int[] Votes { get; set; } = [];
        public string LastUpdatedAt { get; set; }
    }

    public class Preferences
    {
        public bool BannerClosed { get; set; }
        public bool OriginalPhotoFirst { get; set; }
    }
}
