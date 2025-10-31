using Microsoft.AspNetCore.Components;
using ThenAndNow.Constants;
using ThenAndNow.Helpers;
using ThenAndNow.Interfaces;
using ThenAndNow.Models;

namespace ThenAndNow.Services
{
    public class NavigationService(NavigationManager navigationManager) : INavigationService
    {
        public void Navigate(QueryParams queryParams)
        {
            var queryString = queryParams.ToQueryString();

            navigationManager.NavigateTo(queryString);
        }

        public void NavigateToEntry(int id)
        {
            navigationManager.NavigateTo($"{Routes.Entry}?id={id}");
        }
    }
}
