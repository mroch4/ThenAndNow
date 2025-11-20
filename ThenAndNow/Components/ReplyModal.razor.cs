using Microsoft.AspNetCore.Components;
using ThenAndNow.Interfaces;
using ThenAndNow.Models.DTO;

namespace ThenAndNow.Components
{
    public partial class ReplyModal
    {
        #region Dependency Injection

        [Inject]
        private IReplyService ReplyService { get; set; }

        #endregion

        #region Blazor Overrides

        protected override Task OnInitializedAsync()
        {
            ReplyService.OnReplyChanged += StateHasChanged;
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            ReplyService.OnReplyChanged -= StateHasChanged;
        }

        #endregion

        public Reply Reply => ReplyService.Reply ?? new Reply();

        #region Private members

        private bool Disabled => string.IsNullOrEmpty(Reply.Name) ||
                                 Reply.Name.Length > MaxNameLength ||
                                 string.IsNullOrEmpty(Reply.Content) ||
                                 Reply.Content.Length > MaxContentLength;

        private const int MaxContentLength = 500;
        private const int MaxNameLength = 25;

        private async Task AddReply()
        {
            await ReplyService.AddReply();
        }

        private void Reset()
        {
            ReplyService.Reply.Content = null;
        }

        private void OnContentInput(ChangeEventArgs e)
        {
            var content = e.Value?.ToString() ?? string.Empty;
            ReplyService.Reply.Content = content;
            StateHasChanged();
        }

        #endregion
    }
}