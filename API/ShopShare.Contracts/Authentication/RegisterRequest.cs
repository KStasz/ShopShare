namespace ShopShare.Contracts.Authentication
{
    public record RegisterRequest(
        string UserName,
        string Email, 
        string FirstName, 
        string LastName, 
        string Password);
}
