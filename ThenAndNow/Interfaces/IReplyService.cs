using ThenAndNow.Models.DTO;

namespace ThenAndNow.Interfaces
{
    public interface IReplyService
    {
        public Reply Reply { get; set; }

        public Task AddReply();
        public Task<Reply[]> GetRepliesById(int id);
        public Task ShowModal(Reply reply);

        public event Action OnReplyAdded;
        public event Action OnReplyChanged;
    }
}
