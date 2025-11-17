using ThenAndNow.Models;
using ThenAndNow.Models.DTO;

namespace ThenAndNow.Interfaces
{
    public interface IFirebaseService
    {
        #region Auth

        public Task<User> GetCurrentUser();
        public Task<User> SignInWithGoogle();
        public Task SignOut();

        #endregion

        #region Rating

        public Task<Rating> GetRatingById(int id);

        public Task<(bool Success, Rating Rating)> ThumbsDown(int id);

        public Task<(bool Success, Rating Rating)> ThumbsUp(int id);

        #endregion

        #region Replies

        public Task<bool> AddReply(Reply reply);

        public Task<Reply[]> GetRepliesById(int id);

        #endregion
    }
}
