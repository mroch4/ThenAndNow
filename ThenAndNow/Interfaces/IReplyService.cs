using ThenAndNow.Models.DTO;

namespace ThenAndNow.Interfaces
{
    public interface IReplyService
    {
        public Reply Reply { get; set; }

        public Task<(bool Success, Reply Reply)> AddReply();

        public Task<Reply[]> GetRepliesById(int id);

        public Task<Reply> SetReply(int id);

        public Task ShowModal();
    }
}
