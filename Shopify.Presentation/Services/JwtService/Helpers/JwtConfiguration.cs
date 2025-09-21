namespace Shopify.Presentation.Services.JwtService.Helpers
{
    public class JwtConfiguration
    {
        public string Audiance { get; set; }
        public string Issuer { get; set; }
        public string Secret { get; set; }
        public int Expiry { get; set; }
    }
}
