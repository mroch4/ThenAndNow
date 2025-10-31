using Microsoft.AspNetCore.Components;
using ThenAndNow.Interfaces;
using ThenAndNow.Models;

namespace ThenAndNow.Components
{
    public partial class Pagination
    {
        #region Parameters

        [Parameter]
        public QueryParams QueryParams { get; set; }

        [Parameter]
        public int PagesCount { get; set; }

        #endregion

        #region Dependency Injection

        [Inject]
        private INavigationService NavigationService { get; set; }

        #endregion

        #region Private UI methods

        private string GetCurrentPageClass(int pageIndex)
        {
            if ((QueryParams.CurrentPage == 0 && pageIndex == 0) ||
                (QueryParams.CurrentPage != 0 && pageIndex == QueryParams.CurrentPage - 1))
            {
                return "current-page-index";
            }

            return null;
        }

        #endregion

        #region Private navigation methods

        private void Next()
        {
            QueryParams.CurrentPage = QueryParams.CurrentPage == 0 ? 2 : ++QueryParams.CurrentPage;
            NavigationService.Navigate(QueryParams);
        }

        private void GoToPage(int pageIndex)
        {
            QueryParams.CurrentPage = pageIndex;
            NavigationService.Navigate(QueryParams);
        }

        private void Previous()
        {
            QueryParams.CurrentPage = --QueryParams.CurrentPage;
            NavigationService.Navigate(QueryParams);
        }

        #endregion
    }
}
