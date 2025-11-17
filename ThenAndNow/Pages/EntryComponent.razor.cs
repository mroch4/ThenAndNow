using Microsoft.AspNetCore.Components;
using ThenAndNow.Constants;
using ThenAndNow.Interfaces;
using ThenAndNow.Models.DTO;

namespace ThenAndNow.Pages
{
    [Route(Routes.Entry)]
    public partial class EntryComponent
    {
        #region Parameters

        [SupplyParameterFromQuery(Name = Routes.EntryIdQueryParamName)]
        private string IdString { get; set; }

        #endregion

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

            Item = await EntryRepository.GetEntryById(int.TryParse(IdString, out var id) ? id : 0);
            DataLoaded = true;
        }

        #endregion

        private Entry Item { get; set; }
        private bool DataLoaded { get; set; }
    }
}
