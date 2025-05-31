namespace Authentication.Service.Models
{
    public class AuthConfiguration
    {
        public string SecretKey { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public int TokenExpirationInHours { get; set; } = 1;
    }
} 