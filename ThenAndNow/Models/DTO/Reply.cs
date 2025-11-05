using System.Text.Json.Serialization;

namespace ThenAndNow.Models.DTO
{
    public class Reply
    {
        [JsonIgnore]
        public int EntryId { get; set; }

        [JsonPropertyName("a")]
        public long Id { get; set; }

        [JsonIgnore]
        public DateTime Timestamp => DateTimeOffset.FromUnixTimeMilliseconds(Id).DateTime;

        [JsonPropertyName("b")]
        public string Name { get; set; }

        [JsonPropertyName("c")]
        public string Email { get; set; }

        [JsonPropertyName("d")]
        public string Content { get; set; }
    }
}
