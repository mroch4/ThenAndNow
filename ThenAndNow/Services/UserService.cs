using ThenAndNow.Interfaces;
using ThenAndNow.Models;

namespace ThenAndNow.Services
{
    public class UserService(IFirebaseService firebaseService, ILocalStorageService localStorageService) : IUserService
    {
        private User User { get; set; }

        #region Authentication

        public async Task<User> GetUserAuth()
        {
            //TODO: Auth
            //User ??= await firebaseService.SignInWithGoogle();
            //User ??= new User { DisplayName = Labels.Author, Email = Labels.Email };
            User ??= new User();
            return User;
        }

        public async Task<User> SignInWithGoogle()
        {
            return await firebaseService.SignInWithGoogle();
        }

        public async Task SignOut()
        {
            await firebaseService.SignOut();
        }

        #endregion

        #region Local

        public async Task<User> GetUser()
        {
            User = await localStorageService.GetItem<User>(UserKey) ?? new User();
            return User;
        }

        public async Task SetUser(User user)
        {
            User = user;
            await localStorageService.SetItem(UserKey, User);
        }

        private const string UserKey = "user";

        #endregion
    }
}
