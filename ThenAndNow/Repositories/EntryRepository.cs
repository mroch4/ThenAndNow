using ThenAndNow.Enums;
using ThenAndNow.Interfaces;
using ThenAndNow.Models.Database;
using ThenAndNow.Models.DTO;

namespace ThenAndNow.Repositories
{
    public class EntryRepository(IFirebaseService firebaseService, IHttpDataCacheService httpDataCacheService) : IEntryRepository
    {
        #region Public methods 

        public async Task<Details> GetDetailsById(int entryId)
        {
            //return await firebaseService.GetDetailsById(entryId);
            await GetDetails();

            return Details.FirstOrDefault(x => x.Id == entryId) ?? new Details();
        }

        public async Task<Response<Entry>> GetEntries(Request query)
        {
            await GetEntries();

            var filteredEntries = Entries.Where(x => string.IsNullOrEmpty(query.Tag) || x.Tags.Contains(query.Tag));

            var response = ApplyOrdering(filteredEntries, query.SortBy, query.SortDirection).ToArray();

            return new Response<Entry>
            {
                Items = response.Skip(query.Skip).Take(query.Take).ToArray(),
                Total = response.Length
            };
        }

        public async Task<int> GetEntriesCount()
        {
            await GetEntries();

            return Entries.Length;
        }

        public async Task<Entry> GetEntryById(int entryId)
        {
            await GetEntries();

            return Entries.FirstOrDefault(x => x.Id == entryId);
        }

        public async Task<Coordinates> GetMapCenter()
        {
            //return await GetAverageCoordinates();
            //return await GetMiddleEntryCoordinates();
            //return await GetMiddleSortedCoordinates();
            return await GetMinMaxCoordinates();
        }

        public async Task<MapEntry[]> GetMapEntries()
        {
            await GetEntries();

            return Entries.Select(x => new MapEntry
            {
                Id = x.Id,
                Title = x.Title,
                Coordinates = x.Coordinates
            }).ToArray();
        }

        public async Task<TagResponse[]> GetTags()
        {
            await GetEntries();

            var tags = Entries.SelectMany(x => x.Tags).ToList();

            return tags.Distinct().Select(tag => new TagResponse
            {
                Tag = tag,
                Count = tags.Count(x => x == tag)
            }).OrderBy(x => x.Tag).ToArray();
        }

        #endregion

        #region Private methods

        private static IEnumerable<Entry> ApplyOrdering(IEnumerable<Entry> entries, SortBy sortBy, SortDirection sortDirection)
        {
            var isAscending = sortDirection == SortDirection.Asc;

            return sortBy switch
            {
                SortBy.Id =>
                    isAscending
                        ? entries.OrderBy(x => x.Id)
                        : entries.OrderByDescending(x => x.Id),

                SortBy.Title =>
                    isAscending
                        ? entries.OrderBy(x => x.Title)
                        : entries.OrderByDescending(x => x.Title),

                SortBy.DateNow =>
                    isAscending
                        ? entries.OrderBy(x => x.Timestamp.Now)
                        : entries.OrderByDescending(x => x.Timestamp.Now),

                _ => entries.OrderByDescending(x => x.Id)
            };
        }

        private async Task GetDetails()
        {
            Details ??= await httpDataCacheService.GetData<Details>(DetailsPath);
        }

        private async Task GetEntries()
        {
            Entries ??= await httpDataCacheService.GetData<Entry>(EntriesPath);
        }

        private async Task<Coordinates> GetAverageCoordinates()
        {
            await GetEntries();

            var coords = Entries.Select(entry => entry.Coordinates).ToList();

            return new Coordinates
            {
                Latitude = coords.Select(x => x.Latitude).Average(),
                Longitude = coords.Select(x => x.Longitude).Average()
            };
        }

        private async Task<Coordinates> GetMiddleEntryCoordinates()
        {
            await GetEntries();

            var index = Entries.Length / 2;

            return new Coordinates
            {
                Latitude = Entries[index].Coordinates.Latitude,
                Longitude = Entries[index].Coordinates.Longitude
            };
        }

        private async Task<Coordinates> GetMiddleSortedCoordinates()
        {
            await GetEntries();

            var index = Entries.Length / 2;

            var coords = Entries.Select(entry => entry.Coordinates).ToList();

            var latitudes = coords.Select(x => x.Latitude).Order().ToList();
            var longitudes = coords.Select(x => x.Longitude).Order().ToList();

            return new Coordinates
            {
                Latitude = latitudes[index],
                Longitude = longitudes[index]
            };
        }

        private async Task<Coordinates> GetMinMaxCoordinates()
        {
            await GetEntries();

            var coords = Entries.Select(entry => entry.Coordinates).ToList();

            var latitudes = coords.Select(x => x.Latitude).ToList();
            var longitudes = coords.Select(x => x.Longitude).ToList();

            return new Coordinates
            {
                Latitude = (latitudes.Max() + latitudes.Min()) / 2,
                Longitude = (longitudes.Max() + longitudes.Min()) / 2
            };
        }

        #endregion

        private Entry[] Entries { get; set; }
        private Details[] Details { get; set; }

        private const string EntriesPath = "json/entries.json";
        //private const string EntriesPath = "json/new.json";
        //private const string EntriesPath = "json/stw.json";

        private const string DetailsPath = "json/details.json";
    }
}