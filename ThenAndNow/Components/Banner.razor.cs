using Microsoft.AspNetCore.Components;
using ThenAndNow.Interfaces;

namespace ThenAndNow.Components
{
    public partial class Banner
    {
        #region Dependency Injection

        [Inject]
        private IUserService UserService { get; set; }

        #endregion

        #region Blazor Overrides

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();

            BannerClosed = await UserService.GetBannerClosed();
        }

        #endregion

        private bool BannerClosed { get; set; }

        private async Task OnClose()
        {
            BannerClosed = await UserService.SetBannerClosed();
        }
    }
}
