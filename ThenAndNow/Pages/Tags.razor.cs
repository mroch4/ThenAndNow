using Microsoft.AspNetCore.Components;
using ThenAndNow.Constants;
using ThenAndNow.Interfaces;
using ThenAndNow.Models.Database;

namespace ThenAndNow.Pages
{
    [Route(Routes.Tags)]
    public partial class Tags
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
            TagsArray = await EntryRepository.GetTags();
        }

        #endregion

        private TagResponse[] TagsArray { get; set; }

        private static double GetFontSize(int count)
        {
            return 12 + 2.5 * count;
        }
    }
}
