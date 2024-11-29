using ShopShare.Application.Services.Mapper;
using ShopShare.Domain.UserAggregate;
using ShopShare.Domain.UserAggregate.ValueObjects;
using ShopShare.Infrastructure.Model;

namespace ShopShare.Infrastructure.Mappers
{
    internal class ApplicationUserToUserAggregateMapper : IMapper<ApplicationUser, User>
    {
        public User Map(ApplicationUser source)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(source.UserName);
            ArgumentException.ThrowIfNullOrWhiteSpace(source.Email);

            return User.Create(
                UserId.Create(source.Id),
                source.UserName,
                source.Email,
                source.FirstName,
                source.LastName,
                source.CreationDate);
        }

        public IEnumerable<User> Map(IEnumerable<ApplicationUser> source)
        {
            return source.Select(Map);
        }
    }
}
