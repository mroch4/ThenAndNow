namespace ThenAndNow.Models.Configuration
{
    public class AppConfiguration
    {
        public string BaseUrl { get; set; }
        public bool DefaultOriginalPhoto { get; set; }
        public FirebaseConfiguration DetailsDb { get; set; }
        public FirebaseConfiguration RatingDb { get; set; }
        public FirebaseConfiguration ReplyDb { get; set; }
    }
}
