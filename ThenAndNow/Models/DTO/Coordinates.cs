using System.Text.Json.Serialization;

namespace ThenAndNow.Models.DTO
{
    public class Coordinates
    {
        [JsonPropertyName("a")]
        public double Latitude { get; set; }

        [JsonPropertyName("b")]
        public double Longitude { get; set; }
    }
}