using Microsoft.AspNetCore.Components;
using ThenAndNow.Interfaces;

namespace ThenAndNow.Components
{
    public partial class Loader
    {
        #region Parameters

        [Parameter]
        public RenderFragment ChildContent { get; set; }

        [Parameter]
        public bool DataLoaded { get; set; }

        [Parameter]
        public bool NotFound { get; set; }

        #endregion

        #region Dependency Injection

        [Inject]
        private INavigationService NavigationService { get; set; }

        #endregion
    }
}