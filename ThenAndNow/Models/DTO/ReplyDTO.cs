using System.Text.Json.Serialization;

namespace ThenAndNow.Models.DTO
{
    public class ReplyDTO
    {
        [JsonPropertyName("a")]
        public long Id { get; set; }

        [JsonPropertyName("b")]
        public string Name { get; set; }

        [JsonPropertyName("c")]
        public string Email { get; set; }

        [JsonPropertyName("d")]
        public string Content { get; set; }

        [JsonPropertyName("e")]
        public string Icon { get; set; }
    }
}