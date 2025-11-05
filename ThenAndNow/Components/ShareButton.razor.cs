using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using ThenAndNow.Constants;
using ThenAndNow.Enums;

namespace ThenAndNow.Components
{
    public partial class ShareButton
    {
        #region Parameters

        [Parameter]
        public string DirectUrl { get; set; }

        [Parameter]
        public SocialMediaType Type { get; set; }

        #endregion

        #region Dependency Injection

        [Inject]
        private IJSRuntime JsRuntime { get; set; }

        #endregion

        #region Blazor Overrides

        protected override void OnInitialized()
        {
            base.OnInitialized();

            Route = Type switch
            {
                SocialMediaType.Copy => null,
                SocialMediaType.Facebook => Routes.Facebook,
                SocialMediaType.LinkedIn => Routes.LinkedIn,
                SocialMediaType.Mailto => Routes.Mailto,
                SocialMediaType.Messenger => Routes.Messenger,
                SocialMediaType.Sms => Routes.Sms,
                SocialMediaType.WhatsUp => Routes.WhatsUp,
                SocialMediaType.XTwitter => Routes.XTwitter,
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        #endregion

        private const int Size = 20;

        private bool Copied { get; set; }
        private string Name => Type.ToString().ToLower();
        private string Route { get; set; }

        private async Task CopyToClipboard()
        {
            await JsRuntime.InvokeVoidAsync("navigator.clipboard.writeText", DirectUrl);
            ToggleCopied(true);

            await Task.Delay(1000);
            ToggleCopied(false);
        }

        private void ToggleCopied(bool value)
        {
            Copied = value;
            StateHasChanged();
        }
    }
}