using ThenAndNow.Interfaces;
using ThenAndNow.Models.DTO;

namespace ThenAndNow.Services
{
    public class ReplyService(IFirebaseService firebaseService) : IReplyService
    {
        public async Task<Reply> AddReply(Reply reply)
        {
            var result = await firebaseService.AddReply(reply);
            return result.Reply;
        }

        public async Task<Reply[]> GetRepliesById(int id)
        {
            return await firebaseService.GetRepliesById(id);
        }
    }
}
