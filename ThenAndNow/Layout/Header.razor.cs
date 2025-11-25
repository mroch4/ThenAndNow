using Microsoft.AspNetCore.Components;
using ThenAndNow.Constants;
using ThenAndNow.Interfaces;
using ThenAndNow.Models.UI;

namespace ThenAndNow.Layout
{
    public partial class Header
    {
        #region Dependency Injection

        [Inject]
        private INavigationService NavigationService { get; set; }

        #endregion

        private static readonly NavItem[] NavItems =
        [
            new()
            {
                Label = Labels.Map,
                Route = Routes.Map
            },
            new()
            {
                Label = Labels.Tags,
                Route = Routes.Tags
            },
            new()
            {
                Label = Labels.Random,
                Route = Routes.Random
            },
            new()
            {
                Label = Labels.Contact,
                Route = Routes.Contact
            }
        ];
    }
}
