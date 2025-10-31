namespace ThenAndNow.Interfaces
{
    public interface IHttpDataCacheService
    {
        Task<T[]> GetData<T>(string jsonPath);
    }
}