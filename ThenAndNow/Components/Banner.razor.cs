using Microsoft.AspNetCore.Components;
using ThenAndNow.Constants;
using ThenAndNow.Interfaces;

namespace ThenAndNow.Components
{
    public partial class Banner
    {
        #region Dependency Injection

        [Inject]
        private ILocalStorageService LocalStorageService { get; set; }

        #endregion

        #region Blazor Overrides

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();

            BannerClosedByUser = await GetValue();
        }

        #endregion

        private bool BannerClosedByUser { get; set; }

        private async Task<bool> GetValue()
        {
            return await LocalStorageService.GetItem<bool>(LocalStorageKeys.BannerClosedByUser);
        }

        private async Task OnClose()
        {
            await LocalStorageService.SetItem(LocalStorageKeys.BannerClosedByUser, true);
            BannerClosedByUser = await GetValue();
        }
    }
}
