using Microsoft.JSInterop;
using ThenAndNow.Constants;
using ThenAndNow.Interfaces;
using ThenAndNow.Models.DTO;

namespace ThenAndNow.Services
{
    public class ReplyService(IFirebaseService firebaseService, IJSRuntime jsRuntime, IUserService userService) : IReplyService
    {
        public Reply Reply { get; set; }

        public async Task AddReply()
        {
            var user = await userService.GetUser();

            if (string.IsNullOrEmpty(user.Name))
            {
                user.Name = Reply.Name;
                await userService.SetUser(user);
            }

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

        public async Task ShowModal(int id)
        {
            Reply = await SetReply(id);
            OnReplyChanged?.Invoke();

            await jsRuntime.InvokeVoidAsync(JsInteropKeys.ShowModal, Constants.Constants.ReplyModalId);
        }

        public event Action OnReplyAdded;
        public event Action OnReplyChanged;

        #region Private members

        private static string GetRandomColour()
        {
            var random = new Random();
            return Colours[random.Next(Colours.Length)];
        }

        private async Task<Reply> SetReply(int id)
        {
            var user = await userService.GetUser();

            if (string.IsNullOrEmpty(user.Color))
            {
                user.Color = GetRandomColour();
                await userService.SetUser(user);
            }

            return new Reply
            {
                Name = user.Name,
                Email = user.Email,
                Color = user.Color,
                EntryId = id
            };
        }

        private async Task SetUserName()
        {

        }

        private static readonly string[] Colours = [
            "2980B9",
            "3498DB",
            "D35400",
            "27AE60",
            "2ECC71",
            "F39C12",
            "F1C40F",
            "16A085",
            "1ABC9C",
            "7F8C8D",
            "95A5A6",
            "2C3E50",
            "34495E",
            "BDC3C7",
            "8E44AD",
            "9B59B6",
            "C0392B",
            "E74C3C",
            "A94136",
            "B49255",
            "9BA37E",
            "F69785",
            "D870AD",
            "EC87BF"
        ];

        #endregion
    }
}