using Microsoft.AspNetCore.Components;
using ThenAndNow.Constants;
using ThenAndNow.Interfaces;
using ThenAndNow.Models.Database;

namespace ThenAndNow.Pages
{
    [Route(Routes.Tags)]
    public partial class TagsComponent
    {
        #region Dependency Injection

        [Inject]
        private IEntryRepository EntryRepository { get; set; }

        [Inject]
        private INavigationService NavigationService { get; set; }

        #endregion

        #region Blazor Overrides

        protected override async Task OnInitializedAsync()
        {
            Tags = await EntryRepository.GetTags();
            DataLoaded = true;
        }

        #endregion

        private TagResponse[] Tags { get; set; }
        private bool DataLoaded { get; set; }

        private static double GetFontSize(int count)
        {
            return 12 + 2.5 * count;
        }
    }
}
