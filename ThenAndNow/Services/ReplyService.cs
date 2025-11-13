using Microsoft.JSInterop;
using ThenAndNow.Constants;
using ThenAndNow.Interfaces;
using ThenAndNow.Models.DTO;

namespace ThenAndNow.Services
{
    public class ReplyService(
        IJSRuntime jsRuntime,
        IFirebaseService firebaseService) : IReplyService
    {
        public Reply Reply { get; set; }

        public async Task<(bool, Reply)> AddReply()
        {
            Reply.Id = DateTimeOffset.Now.ToUnixTimeMilliseconds();
            return await firebaseService.AddReply(Reply);
        }

        public async Task<Reply[]> GetRepliesById(int id)
        {
            return await firebaseService.GetRepliesById(id);
        }

        public async Task ShowModal(Reply reply)
        {
            Reply = reply;
            OnChange?.Invoke();

            await jsRuntime.InvokeVoidAsync(JsInteropKeys.ShowModal, Constants.Constants.ReplyModalId);
        }

        public event Action OnChange;
    }
}