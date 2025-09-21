namespace Shopify.Presentation.Services.JwtService
{
    public class AuthenticationResult
    {
        public string UserId { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Token { get; set; }
    }
}
