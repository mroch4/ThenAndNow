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

            return user.IsValid
                ? new Reply { EntryId = Id, Name = user.DisplayName, Email = user.Email, Content = Guid.NewGuid().ToString() }
                : null;
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
    }
}