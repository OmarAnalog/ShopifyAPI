namespace Shopify.Infrastructure.Persistence.Configurations
{
    public class DatabaseSettings
    {
        public string Host { get; set; } = "localhost";
        public int Port { get; set; } = 5432;
        public string Name { get; set; } = "Shopify";
        public string User { get; set; } = "postgres";
        public string Password { get; set; } = string.Empty;
    }
}
