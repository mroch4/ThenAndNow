using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using ThenAndNow.Constants;
using ThenAndNow.Interfaces;
using ThenAndNow.Models.DTO;

namespace ThenAndNow.Pages
{
    [Route(Routes.Map)]
    public partial class Map : IDisposable
    {
        #region Dependency Injection

        [Inject]
        private IEntryRepository EntryRepository { get; set; }

        [Inject]
        private IJSRuntime JsRuntime { get; set; }

        #endregion

        #region Blazor Overrides

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();

            MapEntries = await EntryRepository.GetMapEntries();
            MapCenter = await EntryRepository.GetMapCenter();
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                var dotNetObjectReference = DotNetObjectReference.Create(this);
                await JsRuntime.InvokeVoidAsync(JsInteropKeys.InitMap, MapCenter.Latitude, MapCenter.Longitude, MapEntries, dotNetObjectReference);
            }
        }

        #endregion

        #region JavaScript Callbacks

        [JSInvokable]
        public async Task OnMarkerOpened(int entryId)
        {
            Entry = await EntryRepository.GetEntryById(entryId);
            StateHasChanged();
        }

        [JSInvokable]
        public void OnMarkerClosed()
        {
            Entry = null;
            StateHasChanged();
        }

        #endregion

        #region IDisposable

        public void Dispose()
        {
            // Clean up the DotNetObjectReference if needed
        }

        #endregion

        private Entry Entry { get; set; }
        private MapEntry[] MapEntries { get; set; } = [];
        private Coordinates MapCenter { get; set; }
    }
}