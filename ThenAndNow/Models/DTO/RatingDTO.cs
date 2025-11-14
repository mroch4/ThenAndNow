using System.Text.Json.Serialization;

namespace ThenAndNow.Models.DTO
{
    public class RatingDTO
    {
        [JsonPropertyName("a")]
        public int Score { get; set; }

        [JsonPropertyName("b")]
        public int Total { get; set; }
    }
}