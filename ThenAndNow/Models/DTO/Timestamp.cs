using System.Globalization;
using System.Text.Json.Serialization;

namespace ThenAndNow.Models.DTO
{
    public class Timestamp
    {
        [JsonPropertyName("a")]
        public string Then { get; set; }

        [JsonPropertyName("b")]
        public long NowNumber { get; set; }

        [JsonIgnore]
        public DateTime Now => DateTime.TryParseExact($"20{NowNumber}", Constants.Constants.EntryDateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out var result) ? result : new DateTime();
    }
}