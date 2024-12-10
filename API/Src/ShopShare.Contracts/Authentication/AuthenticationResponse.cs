using ShopShare.Contracts.Users;

namespace ShopShare.Contracts.Authentication
{
    public record AuthenticationResponse(
        UserResponse User,
        string Token);
}
