namespace ThenAndNow.Models
{
    public class User
    {
        #region User Info

        public string Name { get; set; }
        public string Icon { get; set; }

        #endregion

        public Preferences Preferences { get; set; } = new();
        public int[] Votes { get; set; } = [];

        public string LastUpdate { get; set; }
    }

    public class Preferences
    {
        public bool BannerClosed { get; set; }
        public bool OriginalPhotoFirst { get; set; }
    }
}
