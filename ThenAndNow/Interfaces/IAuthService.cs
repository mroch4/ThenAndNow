using ThenAndNow.Models;

namespace ThenAndNow.Interfaces
{
    public interface IAuthService
    {
        public Task<User> GetCurrentUser();
        public Task<User> SignInWithGoogle();
        public Task SignOut();
    }
}
