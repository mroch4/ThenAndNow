using Microsoft.AspNetCore.Components;
using ThenAndNow.Interfaces;
using ThenAndNow.Models.DTO;

namespace ThenAndNow.Components
{
    public partial class ReplyModal
    {
        [Inject]
        private IAuthService AuthService { get; set; }

        [Inject]
        private IReplyService ReplyService { get; set; }

        public Reply Reply => ReplyService.Reply ?? new Reply();

        private async Task AddReply()
        {
            await ReplyService.AddReply();
            StateHasChanged();
        }

        private void Reset()
        {
            if (ReplyService.Reply != null)
            {
                ReplyService.Reply.Content = null;
            }
            StateHasChanged();
        }
    }
}