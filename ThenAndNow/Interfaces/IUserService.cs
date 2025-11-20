using ThenAndNow.Models;

namespace ThenAndNow.Interfaces
{
    public interface IUserService
    {
        #region Authentication

        public Task<User> GetUserAuth();
        public Task<User> SignInWithGoogle();
        public Task SignOut();

        #endregion

        #region Local

        public Task<User> GetUser();
        public Task SetUser(User user);

        #endregion
    }
}
