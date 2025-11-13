using Microsoft.JSInterop;
using ThenAndNow.Constants;
using ThenAndNow.Interfaces;
using ThenAndNow.Models;
using ThenAndNow.Models.DTO;

namespace ThenAndNow.Services
{
    public class ReplyService(
        IJSRuntime jsRuntime,
        IFirebaseService firebaseService) : IReplyService
    {
        public Reply Reply { get; set; } = new();

        public async Task<(bool, Reply)> AddReply()
        {
            Reply.Id = DateTimeOffset.Now.ToUnixTimeMilliseconds();
            return await firebaseService.AddReply(Reply);
        }

        public async Task<Reply[]> GetRepliesById(int id)
        {
            return await firebaseService.GetRepliesById(id);
        }

        public async Task<Reply> SetReply(int id)
        {
            //user ??= await authService.GetCurrentUser();
            var user = new User { DisplayName = Labels.Author, Email = Labels.Email };

            Reply = new Reply { EntryId = id };

            if (!user.IsValid) return null;

            Reply.Name = user.DisplayName;
            Reply.Email = user.Email;

            return Reply;
        }

        public async Task ShowModal()
        {
            await jsRuntime.InvokeVoidAsync("bootstrapInterop.showModal", Constants.Constants.ReplyModalId);
        }
    }
}