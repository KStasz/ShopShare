using ShopShare.Domain.UserAggregate;

namespace ShopShare.Application.Authentication.Models
{
    public record AuthenticationResult(User user, string token);
}
