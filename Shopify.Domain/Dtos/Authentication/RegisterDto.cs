namespace Shopify.Domain.Dtos.Authentication
{
    public record RegisterDto
    {
        public string FirstName { get; init; }
        public string LastName { get; init; }
        public string UserName { get; set; }
        public string Email { get; init; }
        public string Password { get; init; }
        public IEnumerable<string>? Roles { get; init; }
    }
}
