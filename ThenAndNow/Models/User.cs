namespace ThenAndNow.Models
{
    public class User
    {
        public string DisplayName { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }

        public bool IsValid =>
            !string.IsNullOrEmpty(DisplayName) &&
            !string.IsNullOrEmpty(Email) &&
            !string.IsNullOrEmpty(Token);
    }
}
