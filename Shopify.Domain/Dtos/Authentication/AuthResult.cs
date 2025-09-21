namespace Shopify.Domain.Dtos.Authentication;

public class AuthResult
{
    public string Token { get; set; } = null!;
    public string UserId { get; set; } = null!;
    public string FirstName { get; set; } = null!;
    public string UserName { get; set; } = null!;
}
