using Microsoft.AspNetCore.Components;
using ThenAndNow.Constants;
using ThenAndNow.Models.Configuration;

namespace ThenAndNow.Components
{
    public partial class ShareButtons
    {
        #region Parameters

        [Parameter]
        public int Id { get; set; }

        #endregion

        #region Dependency Injection

        [Inject]
        private AppConfiguration AppConfiguration { get; set; }

        #endregion

        private string Url => $"{AppConfiguration.BaseUrl}{Routes.Entry}?{Routes.EntryIdQueryParamName}={Id}";
    }
}