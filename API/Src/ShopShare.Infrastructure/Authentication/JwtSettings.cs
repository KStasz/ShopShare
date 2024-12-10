namespace ShopShare.Infrastructure.Authentication
{
    public class JwtSettings
    {
        public const string JwtSettingsName = "JwtSettings";

        public string Secret { get; set; } = null!;
        public int ExpiryTime { get; set; }
        public string Issuer { get; set; } = null!;
        public string Audience { get; set; } = null!;
    }
}
