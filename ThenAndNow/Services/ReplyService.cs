using Microsoft.JSInterop;
using ThenAndNow.Constants;
using ThenAndNow.Interfaces;
using ThenAndNow.Models.DTO;

namespace ThenAndNow.Services
{
    public class ReplyService(IJSRuntime jsRuntime, IFirebaseService firebaseService) : IReplyService
    {
        public Reply Reply { get; set; }

        public async Task AddReply()
        {
            Reply.Id = DateTimeOffset.Now.ToUnixTimeMilliseconds();

            var result = await firebaseService.AddReply(Reply);
            if (result)
            {
                OnReplyAdded?.Invoke();
            }
        }

        public async Task<Reply[]> GetRepliesById(int id)
        {
            return await firebaseService.GetRepliesById(id);
        }

        public async Task ShowModal(Reply reply)
        {
            Reply = reply;
            OnReplyChanged?.Invoke();

            await jsRuntime.InvokeVoidAsync(JsInteropKeys.ShowModal, Constants.Constants.ReplyModalId);
        }

        public event Action OnReplyAdded;
        public event Action OnReplyChanged;
    }
}