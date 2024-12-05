using ShopShare.Application.Services.Mapper;
using ShopShare.Domain.UserAggregate;
using ShopShare.Domain.UserAggregate.ValueObjects;
using ShopShare.Infrastructure.Model;

namespace ShopShare.Infrastructure.Mappers.Mappers
{
    internal class UserAggregateFunctionToApplicationUserFunctionMapper : IMapper<Func<User, bool>, Func<ApplicationUser, bool>>
    {
        public Func<ApplicationUser, bool> Map(Func<User, bool> source)
        {
            return appUser =>
            {
                ArgumentException.ThrowIfNullOrWhiteSpace(appUser.UserName);
                ArgumentException.ThrowIfNullOrWhiteSpace(appUser.Email);

                return source(
                    User.Create(
                        UserId.Create(appUser.Id),
                        appUser.UserName,
                        appUser.Email,
                        appUser.FirstName,
                        appUser.LastName,
                        appUser.CreationDate));
            };
        }

        public IEnumerable<Func<ApplicationUser, bool>> Map(IEnumerable<Func<User, bool>> source)
        {
            return source.Select(Map);
        }
    }
}
