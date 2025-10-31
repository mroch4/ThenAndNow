using ThenAndNow.Models.Database;
using ThenAndNow.Models.DTO;

namespace ThenAndNow.Interfaces
{
    public interface IEntryRepository
    {
        public Task<Details> GetDetailsById(int id);
        public Task<Response<Entry>> GetEntries(Request query);
        public Task<int> GetEntriesCount();
        public Task<Entry> GetEntryById(int id);
        public Task<Coordinates> GetMapCenter();
        public Task<MapEntry[]> GetMapEntries();
        public Task<TagResponse[]> GetTags();
    }
}