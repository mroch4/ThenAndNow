using ThenAndNow.Interfaces;
using ThenAndNow.Models.DTO;

namespace ThenAndNow.Services
{
    public class ReplyService(IFirebaseService firebaseService) : IReplyService
    {
        public async Task<(bool, Reply)> AddReply(Reply reply)
        {
            return await firebaseService.AddReply(reply);
        }

        public async Task<Reply[]> GetRepliesById(int id)
        {
            return await firebaseService.GetRepliesById(id);
        }
    }
}
