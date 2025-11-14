using System.Text.Json.Serialization;

namespace ThenAndNow.Models.DTO
{
    public class EntryDTO
    {
        [JsonPropertyName("a")]
        public int Id { get; set; }

        [JsonPropertyName("b")]
        public string Title { get; set; }

        [JsonPropertyName("c")]
        public Coordinates Coordinates { get; set; }

        [JsonPropertyName("d")]
        public Timestamp Timestamp { get; set; }
    }
}