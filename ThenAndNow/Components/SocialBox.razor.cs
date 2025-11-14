using Microsoft.AspNetCore.Components;
using ThenAndNow.Enums;
using ThenAndNow.Interfaces;
using ThenAndNow.Models.DTO;

namespace ThenAndNow.Components
{
    public partial class SocialBox
    {
        #region Parameters

        [Parameter]
        public string DirectUrl { get; set; }

        [Parameter]
        public int Id { get; set; }

        #endregion

        #region Dependency Injection

        [Inject]
        private IAuthService AuthService { get; set; }

        [Inject]
        private ILocalStorageService LocalStorageService { get; set; }

        [Inject]
        private IReplyService ReplyService { get; set; }

        #endregion

        private bool ShowReplies { get; set; }
        private Reply[] Replies { get; set; }

        #region Private methods

        private async Task AddReply()
        {
            var reply = await AuthReply();
            if (reply != null)
            {
                await ReplyService.ShowModal(reply);
            }
        }

        private async Task<Reply> AuthReply()
        {
            var user = await AuthService.GetCurrentUser();

            if (!user.IsValid) return null;

            var colour = await LocalStorageService.GetItem<string>(user.Email);

            if (string.IsNullOrEmpty(colour))
            {
                colour = GetRandomColour();
                await LocalStorageService.SetItem(user.Email, colour);
            }

            return new Reply
            {
                EntryId = Id,
                Name = user.DisplayName,
                Email = user.Email,
                Content = Guid.NewGuid().ToString(),
                Color = colour
            };
        }


        private static string GetRandomColour()
        {
            var random = new Random();
            return Colours[random.Next(Colours.Length)];
        }

        private async Task AuthUser()
        {
            var user = await AuthService.SignInWithGoogle();
        }

        private async Task ToggleReplies()
        {
            if (!ShowReplies)
            {
                Replies ??= await ReplyService.GetRepliesById(Id);
            }

            ShowReplies = !ShowReplies;
        }

        #endregion

        public static readonly SocialMediaType[] SocialButtons =
        [
            SocialMediaType.Facebook,
            SocialMediaType.Messenger,
            SocialMediaType.WhatsUp,
            SocialMediaType.XTwitter,
            SocialMediaType.LinkedIn,
            SocialMediaType.Sms,
            SocialMediaType.Mailto,
            SocialMediaType.Copy
        ];

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
    }
}