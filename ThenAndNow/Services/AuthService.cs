using ThenAndNow.Interfaces;
using ThenAndNow.Models;

namespace ThenAndNow.Services
{
    public class AuthService(IFirebaseService firebaseService) : IAuthService
    {
        public User User { get; set; }

        public async Task<User> GetCurrentUser()
        {
            User ??= await firebaseService.SignInWithGoogle();
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
    }
}
