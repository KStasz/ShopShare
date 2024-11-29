using ShopShare.Domain.UserAggregate;

namespace ShopShare.Application.Services
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(User user);
    }
}
