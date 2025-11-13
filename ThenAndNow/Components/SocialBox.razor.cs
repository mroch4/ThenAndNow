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

        private async Task ShowModal()
        {
            var reply = await ReplyService.SetReply(Id);
            if (reply != null)
            {
                await ReplyService.ShowModal();

                ShowReplies = true;
                StateHasChanged();
            }
        }

        private async Task AuthUser()
        {
            var user = await AuthService.GetCurrentUser();
        }

        private async Task GetReplies()
        {
            Replies ??= await ReplyService.GetRepliesById(Id);
        }

        private async Task ToggleReplies()
        {
            await GetReplies();
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