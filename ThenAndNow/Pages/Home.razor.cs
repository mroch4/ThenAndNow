using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using ThenAndNow.Constants;
using ThenAndNow.Enums;
using ThenAndNow.Helpers;
using ThenAndNow.Interfaces;
using ThenAndNow.Models;
using ThenAndNow.Models.Database;
using ThenAndNow.Models.DTO;

namespace ThenAndNow.Pages
{
    [Route(Routes.Root)]
    public partial class Home
    {
        #region Query Parameters

        [SupplyParameterFromQuery(Name = Routes.CurrentPageQueryParamName)]
        private string CurrentPageString { get; set; }

        [SupplyParameterFromQuery(Name = Routes.PageSizeQueryParamName)]
        private string PageSizeString { get; set; }

        [SupplyParameterFromQuery(Name = Routes.SortByQueryParamName)]
        private string SortBy { get; set; }

        [SupplyParameterFromQuery(Name = Routes.SortingQueryParamName)]
        private string SortDirection { get; set; }

        [SupplyParameterFromQuery(Name = Routes.TagQueryParamName)]
        private string Tag { get; set; }

        #endregion

        #region Dependency Injection

        [Inject]
        private IEntryRepository EntryRepository { get; set; }

        [Inject]
        private INavigationService NavigationService { get; set; }

        [Inject]
        private IJSRuntime JsRuntime { get; set; }

        #endregion

        #region Blazor Overrides

        protected override async Task OnParametersSetAsync()
        {
            await base.OnParametersSetAsync();

            QueryParams = new QueryParams(CurrentPageString, PageSizeString, SortingExtensions.ToSorting(SortBy, SortDirection), Tag);

            await LoadEntries();
        }

        #endregion

        private Response<Entry> Entries { get; set; } = new() { Items = [], Total = 0 };

        private QueryParams QueryParams { get; set; }

        private int PagesCount { get; set; }

        public static readonly int[] PageSizes = [1, 2, 3, 4, 5, 6, 8, 10, 12, 15, 20, 30];

        public static (Sorting Value, string Label)[] SortingOptions =
        [
            (Sorting.IdDescending, Labels.IdDescending),
            (Sorting.IdAscending, Labels.IdAscending),
            (Sorting.TitleDescending, Labels.TitleDescending),
            (Sorting.TitleAscending, Labels.TitleAscending),
            (Sorting.DateNowDescending, Labels.DateNowDescending),
            (Sorting.DateNowAscending, Labels.DateNowAscending)
        ];

        #region Private methods

        private async Task LoadEntries()
        {
            var dbQuery = RequestHelper.GetDatabaseQuery(QueryParams);
            var response = await EntryRepository.GetEntries(dbQuery);

            if (response == null || response.Items.Length == 0)
            {
                Entries = null;
                PagesCount = 0;
            }
            else
            {
                Entries = response;
                PagesCount = response.Total.GetPagesCount(dbQuery.Take);
            }

            StateHasChanged();
            await JsRuntime.InvokeVoidAsync(JsInteropKeys.ScrollTop, new { top = 0, behavior = "smooth" });
        }

        private void OnAfterSortingChange()
        {
            QueryParams.CurrentPage = 0;
            NavigationService.Navigate(QueryParams);
        }

        #endregion
    }
}