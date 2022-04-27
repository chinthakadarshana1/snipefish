namespace Snipefish.WebClient.Configurations
{
    public class SnipefishWebConfiguration
    {
        public string SnipefishApiUrl { get; set; } = null!;
        public string SnipefishApiInternalUrl { get; set; } = null!;

        public const string UserSessionKey = "USER_SESSION_KEY";
    }
}
