using System.Text.Json.Serialization;

namespace ThenAndNow.Models.DTO
{
    public class Reply
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime Timestamp { get; set; }
        public string Content { get; set; }
    }

    public class ParentReply : Reply
    {
        [JsonPropertyName("a")]
        public int Id { get; set; }
        private List<ChildReply> Replies { get; set; }
    }

    public class ChildReply : Reply
    {

    }
}
