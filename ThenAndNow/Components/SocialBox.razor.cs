using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
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
        private IJSRuntime JsRuntime { get; set; }

        [Inject]
        private IReplyService ReplyService { get; set; }

        #endregion

        private bool ShowReplies { get; set; }
        private Reply[] Replies { get; set; }
        private bool DataLoaded => ShowReplies && Replies != null;

        private bool ReplyBox => Replies is { Length: > 3 };
        private static string ReplyBoxClassBase => "d-flex flex-column gap-3 p-3 mb-2";
        private string ReplyBoxClass => ReplyBox ? $"{ReplyBoxClassBase} border border-secondary-subtle" : ReplyBoxClassBase;
        private string ReplyBoxStyle => ReplyBox ? "max-height: 300px; overflow-y: auto;" : string.Empty;

        #region Private methods

        private async Task AddReply()
        {
            ReplyService.OnReplyAdded += OnReplyAdded;

            await ReplyService.ShowModal(Id);
        }

        private async void OnReplyAdded()
        {
            var reply = ReplyService.Reply;

            Replies = Replies == null
                ? [reply]
                : Replies.Append(reply).ToArray();

            StateHasChanged();

            await JsRuntime.InvokeVoidAsync(JsInteropKeys.ScrollTo, reply.Id);

            ReplyService.OnReplyAdded -= OnReplyAdded;
        }

        private async Task ToggleReplies()
        {
            if (!ShowReplies)
            {
                ShowReplies = true;
                Replies ??= await ReplyService.GetRepliesById(Id);
            }
            else
            {
                ShowReplies = false;
            }
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
            SocialMediaType.MailTo,
            SocialMediaType.Copy
        ];
    }
}