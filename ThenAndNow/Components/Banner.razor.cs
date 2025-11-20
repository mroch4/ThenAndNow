using Microsoft.AspNetCore.Components;
using ThenAndNow.Interfaces;
using ThenAndNow.Models;

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

            User = await UserService.GetUser();
        }

        #endregion

        private User User { get; set; }

        private async Task OnClose()
        {
            User.ClosedBanner = true;
            await UserService.SetUser(User);
        }
    }
}
