using ThenAndNow.Models.DTO;

namespace ThenAndNow.Interfaces
{
    public interface IReplyService
    {
        public Task<Reply> AddReply(Reply reply);

        public Task<Reply[]> GetRepliesById(int id);
    }
}
