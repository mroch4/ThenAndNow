using ThenAndNow.Interfaces;
using ThenAndNow.Models;

namespace ThenAndNow.Services
{
    public class UserService(ILocalStorageService localStorageService) : IUserService
    {
        private User _user;
        private const string UserKey = nameof(User);

        #region User Preferences

        public async Task<bool> GetBannerClosed()
        {
            var user = await GetUser();
            return user.Preferences.BannerClosed;
        }

        public async Task<bool> SetBannerClosed()
        {
            var user = await GetUser();
            user.Preferences.BannerClosed = true;
            await SetUser();
            return true;
        }

        public async Task<bool> GetOriginalPhotoFirst()
        {
            var user = await GetUser();
            return user.Preferences.OriginalPhotoFirst;
        }

        public async Task<bool> SetOriginalPhotoFirst(bool value)
        {
            var user = await GetUser();
            user.Preferences.OriginalPhotoFirst = value;
            await SetUser();
            return value;
        }

        #endregion

        #region User Properties

        public async Task<string> GetUserIcon()
        {
            var user = await GetUser();
            return user.Icon;
        }

        public async Task<string> SetUserIcon(string color)
        {
            var user = await GetUser();
            user.Icon = color;
            await SetUser();
            return color;
        }

        public async Task<string> GetUserName()
        {
            var user = await GetUser();
            return user.Name;
        }

        public async Task SetUserName(string userName)
        {
            var user = await GetUser();
            user.Name = userName;
            await SetUser();
        }

        #endregion

        public async Task<int[]> GetVotes()
        {
            var user = await GetUser();
            return user.Votes;
        }

        public async Task<bool> GetVotingEnabled(int id)
        {
            var user = await GetUser();
            return !user.Votes.Contains(id);
        }

        public async Task SetVotes(int id)
        {
            if (await GetVotingEnabled(id))
            {
                var user = await GetUser();
                user.Votes = user.Votes.Append(id).ToArray();
                await SetUser();
            }
        }

        #region Private Helpers

        private async Task<User> GetUser()
        {
            return _user ??= await localStorageService.GetItem<User>(UserKey) ?? new User();
        }

        private async Task SetUser()
        {
            if (_user == null) return;

            _user.LastUpdate = DateTime.Now.ToString("G");
            await localStorageService.SetItem(UserKey, _user);
        }

        #endregion
    }
}