using ThenAndNow.Enums;
using ThenAndNow.Interfaces;
using ThenAndNow.Models.Database;
using ThenAndNow.Models.DTO;

namespace ThenAndNow.Repositories
{
    public class EntryRepository(IHttpDataCacheService httpDataCacheService) : IEntryRepository
    {
        #region Public methods 

        public async Task<Details> GetDetailsById(int entryId)
        {
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
            await GetEntries();

            //return GetAverageCoordinates();
            //return GetMiddleEntryCoordinates();
            //return GetMiddleSortedCoordinates();
            return GetMinMaxCoordinates();
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

            var tags = Details.SelectMany(x => x.Tags).ToList();

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
                        ? entries.OrderBy(x => x.Title).ThenBy(x => x.Id)
                        : entries.OrderByDescending(x => x.Title).ThenBy(x => x.Id),

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
            if (Entries != null) return;

            var entries = await httpDataCacheService.GetData<Entry>(EntriesPath);

            if (Details == null) await GetDetails();

            var detailsMap = Details!.ToDictionary(d => d.Id);

            foreach (var entry in entries)
            {
                if (detailsMap.TryGetValue(entry.Id, out var detail))
                {
                    entry.Description = detail.Description;
                    entry.Tags = detail.Tags;
                }
            }

            Entries = entries;
        }

        private Coordinates GetAverageCoordinates()
        {
            var coords = Entries.Select(entry => entry.Coordinates).ToList();

            return new Coordinates
            {
                Latitude = coords.Select(x => x.Latitude).Average(),
                Longitude = coords.Select(x => x.Longitude).Average()
            };
        }

        private Coordinates GetMiddleEntryCoordinates()
        {
            var index = Entries.Length / 2;

            return new Coordinates
            {
                Latitude = Entries[index].Coordinates.Latitude,
                Longitude = Entries[index].Coordinates.Longitude
            };
        }

        private Coordinates GetMiddleSortedCoordinates()
        {
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

        private Coordinates GetMinMaxCoordinates()
        {
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
        private const string DetailsPath = "json/details.json";
    }
}