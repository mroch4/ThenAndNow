using Microsoft.AspNetCore.Components;
using ThenAndNow.Interfaces;

namespace ThenAndNow.Components
{
    public partial class Loader
    {
        [Parameter]
        public RenderFragment ChildContent { get; set; }

        [Parameter]
        public bool DataLoaded { get; set; }

        [Parameter]
        public bool NotFound { get; set; }

        [Inject]
        private INavigationService NavigationService { get; set; }
    }
}