using Microsoft.AspNetCore.Components;
using ThenAndNow.Interfaces;
using ThenAndNow.Models.DTO;

namespace ThenAndNow.Components
{
    public partial class ReplyModal
    {
        #region Dependency Injection

        [Inject]
        private IAuthService AuthService { get; set; }

        [Inject]
        private IReplyService ReplyService { get; set; }

        #endregion

        #region Blazor Overrides

        protected override Task OnInitializedAsync()
        {
            ReplyService.OnChange += StateHasChanged;
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            ReplyService.OnChange -= StateHasChanged;
        }

        #endregion

        public Reply Reply => ReplyService.Reply ?? new Reply();

        private async Task AddReply()
        {
            await ReplyService.AddReply();
        }

        private void Reset()
        {
            ReplyService.Reply.Content = null;
        }

        #region Private members

        private bool Disabled => string.IsNullOrEmpty(Reply.Content) || Reply.Content.Length > MaxContentLength;

        private const int MaxContentLength = 500;

        #endregion
    }
}