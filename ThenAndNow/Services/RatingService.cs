using ThenAndNow.Constants;
using ThenAndNow.Interfaces;
using ThenAndNow.Models.DTO;

namespace ThenAndNow.Services
{
    public class RatingService(IFirebaseService firebaseService, ILocalStorageService localStorageService) : IRatingService
    {
        #region Public methods

        public async Task<Rating> GetRatingById(int id)
        {
            return await firebaseService.GetRatingById(id);
        }

        public async Task<bool> RatingEnabled(int id)
        {
            var userVotes = await GetUserVotes();

            return !userVotes.Contains(id);
        }

        public async Task<Rating> ThumbsDown(int id)
        {
            var result = await firebaseService.ThumbsDown(id);

            if (result.Success)
            {
                await UpdateUserVotes(id);
            }

            return result.Rating;
        }

        public async Task<Rating> ThumbsUp(int id)
        {
            var result = await firebaseService.ThumbsUp(id);

            if (result.Success)
            {
                await UpdateUserVotes(id);
            }

            return result.Rating;
        }

        #endregion

        #region Private methods

        private async Task<int[]> GetUserVotes()
        {
            return await localStorageService.GetItem<int[]>(LocalStorageKeys.UserVotes) ?? [];
        }

        private async Task UpdateUserVotes(int id)
        {
            var userVotes = await GetUserVotes();

            if (await RatingEnabled(id))
            {
                await localStorageService.SetItem(LocalStorageKeys.UserVotes, userVotes.Append(id).ToArray());
            }
        }

        #endregion
    }
}
