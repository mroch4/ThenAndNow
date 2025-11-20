using System.Globalization;
using System.Text.Json.Serialization;

namespace ThenAndNow.Models.DTO
{
    public class Reply : ReplyDTO
    {
        [JsonIgnore]
        public int EntryId { get; set; }

        [JsonIgnore]
        public DateTime Timestamp => DateTimeOffset.FromUnixTimeMilliseconds(Id).DateTime;

        [JsonIgnore]
        public string TimestampString => Timestamp.ToString("F", new CultureInfo(Constants.Constants.CultureInfo));

        [JsonIgnore]
        public string Initial => Name[0].ToString().ToUpper();
    }
}
