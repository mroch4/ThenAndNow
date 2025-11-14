using System.Text.Json.Serialization;

namespace ThenAndNow.Models.DTO
{
    public class Rating : RatingDTO
    {
        [JsonIgnore]
        public int Id { get; set; }
    }
}
