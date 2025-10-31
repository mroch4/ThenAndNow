using System.Text.Json.Serialization;

namespace ThenAndNow.Models.DTO
{
    public class MapEntry
    {
        [JsonPropertyName("a")]
        public int Id { get; set; }

        [JsonPropertyName("b")]
        public Coordinates Coordinates { get; set; }

        [JsonPropertyName("e")]
        public string Title { get; set; }
    }
}