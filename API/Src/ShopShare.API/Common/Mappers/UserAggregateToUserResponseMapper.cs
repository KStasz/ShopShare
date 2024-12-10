using ShopShare.Application.Services.Mapper;
using ShopShare.Contracts.Users;
using ShopShare.Domain.UserAggregate;

namespace ShopShare.API.Common.Mappers
{
    public class UserAggregateToUserResponseMapper
        : IMapper<User, UserResponse>
    {
        public UserResponse Map(User source)
        {
            return new UserResponse(
                source.Id.Value,
                source.UserName,
                source.Email,
                source.FirstName,
                source.LastName,
                source.CreationDate,
                source.UserRoles
                    .Select(x => x.Value)
                    .ToList());
        }

        public IEnumerable<UserResponse> Map(IEnumerable<User> source)
        {
            return source.Select(Map);
        }
    }
}
