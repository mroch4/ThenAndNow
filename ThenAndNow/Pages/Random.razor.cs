using Microsoft.AspNetCore.Components;
using ThenAndNow.Constants;
using ThenAndNow.Interfaces;

namespace ThenAndNow.Pages
{
    [Route(Routes.Random)]
    public partial class Random
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
            await base.OnInitializedAsync();

            var totalCount = await EntryRepository.GetEntriesCount();
            var random = new System.Random();
            var randomId = random.Next(1, totalCount);

            NavigationService.NavigateToEntry(randomId);
        }

        #endregion
    }
}
