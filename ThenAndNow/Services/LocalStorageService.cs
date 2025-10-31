using Microsoft.JSInterop;
using System.Text.Json;
using ThenAndNow.Constants;
using ThenAndNow.Interfaces;

namespace ThenAndNow.Services
{
    public class LocalStorageService(IJSRuntime jsRuntime) : ILocalStorageService
    {
        public async Task<T> GetItem<T>(string key)
        {
            try
            {
                var json = await jsRuntime.InvokeAsync<string>(JsInteropKeys.GetItem, key);
                return string.IsNullOrEmpty(json) ? default : JsonSerializer.Deserialize<T>(json);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"LocalStorageService.GetItemAsync error: {ex.Message}");
                return default;
            }
        }

        public async Task SetItem<T>(string key, T value)
        {
            try
            {
                var json = JsonSerializer.Serialize(value);
                await jsRuntime.InvokeVoidAsync(JsInteropKeys.SetItem, key, json);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"LocalStorageService.SetItemAsync error: {ex.Message}");
            }
        }
    }
}