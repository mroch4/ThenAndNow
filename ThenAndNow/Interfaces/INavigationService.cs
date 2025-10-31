using ThenAndNow.Models;

namespace ThenAndNow.Interfaces
{
    public interface INavigationService
    {
        public void Navigate(QueryParams queryParams);
        public void NavigateToEntry(int id);
    }
}