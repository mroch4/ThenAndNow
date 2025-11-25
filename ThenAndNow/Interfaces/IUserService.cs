namespace ThenAndNow.Interfaces
{
    public interface IUserService
    {
        #region User Preferences

        public Task<bool> GetBannerClosed();
        public Task<bool> SetBannerClosed();
        public Task<bool> GetOriginalPhotoFirst();
        public Task<bool> SetOriginalPhotoFirst(bool value);

        #endregion

        #region User Properties

        public Task<string> GetUserIcon();
        public Task<string> SetUserIcon(string color);
        public Task<string> GetUserName();
        public Task SetUserName(string userName);

        #endregion

        public Task<int[]> GetVotes();
        public Task<bool> GetVotingEnabled(int id);
        public Task SetVotes(int id);
    }
}
