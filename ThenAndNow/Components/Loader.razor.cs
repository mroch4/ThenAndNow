using Microsoft.AspNetCore.Components;
using ThenAndNow.Interfaces;

namespace ThenAndNow.Components
{
    public partial class Loader
    {
        [Parameter]
        public RenderFragment ChildContent { get; set; }

        [Parameter]
        public bool Loaded { get; set; }

        [Parameter]
        public bool NoData { get; set; }

        [Inject]
        private INavigationService NavigationService { get; set; }
    }
}