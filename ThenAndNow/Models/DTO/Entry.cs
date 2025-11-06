using System.Globalization;
using System.Text.Json.Serialization;

namespace ThenAndNow.Models.DTO
{
    public class Entry
    {
        [JsonPropertyName("a")]
        public int Id { get; set; }

        [JsonPropertyName("b")]
        public string Title { get; set; }

        [JsonPropertyName("c")]
        public Coordinates Coordinates { get; set; }

        [JsonPropertyName("d")]
        public Timestamp Timestamp { get; set; }

        [JsonIgnore]
        public string Description { get; set; }

        [JsonIgnore]
        public string[] Tags { get; set; }
    }

    public class Coordinates
    {
        [JsonPropertyName("a")]
        public double Latitude { get; set; }

        [JsonPropertyName("b")]
        public double Longitude { get; set; }
    }

    public class Timestamp
    {
        [JsonPropertyName("a")]
        public string Then { get; set; }

        [JsonPropertyName("b")]
        public long NowNumber { get; set; }

        [JsonIgnore]
        public DateTime Now => DateTime.TryParseExact($"20{NowNumber}", Constants.Constants.EntryDateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out var result) ? result : new DateTime();
    }

    public class Details
    {
        [JsonPropertyName("a")]
        public int Id { get; set; }

        [JsonPropertyName("b")]
        public string Description { get; set; }

        [JsonPropertyName("c")]
        public string[] Tags { get; set; }
    }
}
