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
            await SetUserName();

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
            Reply = new Reply
            {
                EntryId = id,
                Icon = await GetUserIcon(),
                Name = await userService.GetUserName()
            };

            OnReplyChanged?.Invoke();

            await jsRuntime.InvokeVoidAsync(JsInteropKeys.ShowModal, Constants.Constants.ReplyModalId);
        }

        public event Action OnReplyAdded;
        public event Action OnReplyChanged;

        #region Private members

        private async Task<string> GetUserIcon()
        {
            var icon = await userService.GetUserIcon();

            return string.IsNullOrEmpty(icon)
                ? await userService.SetUserIcon(GetRandomIconColor())
                : icon;

            static string GetRandomIconColor()
            {
                var random = new Random();
                return IconColors[random.Next(IconColors.Length)];
            }
        }

        private async Task SetUserName()
        {
            var name = await userService.GetUserName();

            if (string.IsNullOrEmpty(name) || !string.Equals(name, Reply.Name))
            {
                await userService.SetUserName(Reply.Name);
            }
        }

        private static readonly string[] IconColors = [
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