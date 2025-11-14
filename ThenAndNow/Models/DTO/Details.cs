using System.Text.Json.Serialization;

namespace ThenAndNow.Models.DTO
{
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