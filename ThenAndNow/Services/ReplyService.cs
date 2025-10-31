using ThenAndNow.Interfaces;
using ThenAndNow.Models.Configuration;

namespace ThenAndNow.Services
{
    public class ReplyService(AppConfiguration appConfiguration, IFirebaseService firebaseService) : IReplyService
    {
        public async Task AddReply(int id)
        {
            await firebaseService.AddReply(id);
        }
    }
}
