namespace EnterpriseApp.API.Core.Authentication
{
    public class AuthConfig
    {
        public string Secret { get; set; }
        public int ExpirationHours { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
    }
}
