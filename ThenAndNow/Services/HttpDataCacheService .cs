using System.Net.Http.Json;
using ThenAndNow.Interfaces;

namespace ThenAndNow.Services
{
    public class HttpDataCacheCacheService(HttpClient httpClient) : IHttpDataCacheService
    {
        private readonly Dictionary<string, object> _cachedData = new();
        private readonly SemaphoreSlim _semaphore = new(1, 1);

        public async Task<T[]> GetData<T>(string path)
        {
            if (_cachedData.TryGetValue(path, out var cached) && cached is T[] cachedArray)
            {
                return cachedArray;
            }

            await _semaphore.WaitAsync();

            try
            {
                if (_cachedData.TryGetValue(path, out cached) && cached is T[] cachedArrayAfterWait)
                {
                    return cachedArrayAfterWait;
                }

                var data = await httpClient.GetFromJsonAsync<T[]>(path);
                var result = data ?? [];
                _cachedData[path] = result;
                return result;
            }
            finally
            {
                _semaphore.Release();
            }
        }
    }
}