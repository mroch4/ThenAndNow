using System.Text.Json.Serialization;

namespace ThenAndNow.Models.DTO
{
    public class Rating
    {
        [JsonIgnore]
        public int Id { get; set; }

        [JsonPropertyName("a")]
        public int Score { get; set; }

        [JsonPropertyName("b")]
        public int Total { get; set; }
    }
}
