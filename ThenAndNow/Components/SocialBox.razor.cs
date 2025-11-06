using Microsoft.AspNetCore.Components;
using ThenAndNow.Constants;
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
        private IReplyService ReplyService { get; set; }

        #endregion

        private bool ShowReplies { get; set; }
        private Reply[] Replies { get; set; }

        #region Private methods

        private async Task AddReply()
        {
            var result = await ReplyService.AddReply(new Reply
            {
                EntryId = Id,
                Id = DateTimeOffset.Now.ToUnixTimeMilliseconds(),
                Name = Labels.Author,
                Email = Labels.Email,
                Content = Guid.NewGuid().ToString()
            });

            if (result.Success)
            {
                Replies = Replies.Append(result.Reply).ToArray();
                ShowReplies = true;
                StateHasChanged();
            }
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