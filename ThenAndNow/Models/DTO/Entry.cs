using System.Text.Json.Serialization;

namespace ThenAndNow.Models.DTO
{
    public class Entry
    {
        [JsonPropertyName("a")]
        public int Id { get; set; }

        [JsonPropertyName("b")]
        public Coordinates Coordinates { get; set; }

        [JsonPropertyName("c")]
        public Timestamp Timestamp { get; set; }

        [JsonPropertyName("d")]
        public string[] Tags { get; set; }

        [JsonPropertyName("e")]
        public string Title { get; set; }

        [JsonIgnore]
        public string Description { get; set; }
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
        public string NowString { get; set; }

        [JsonIgnore]
        public DateTime Now => DateTime.TryParse($"20{NowString}", out var result) ? result : new DateTime();
    }

    public class Details
    {
        [JsonPropertyName("a")]
        public string Description { get; set; }
    }
}
