using ThenAndNow.Models;

namespace ThenAndNow.Interfaces
{
    public interface IAuthService
    {
        public User User { get; set; }
        public Task<User> GetCurrentUser();
        public Task<User> SignInWithEmail();
        public Task<User> SignInWithFacebook();
        public Task<User> SignInWithGoogle();
        public Task SignOut();
    }
}
