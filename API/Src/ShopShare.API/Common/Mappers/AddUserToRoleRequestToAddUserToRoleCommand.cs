using ShopShare.Application.Services.Mapper;
using ShopShare.Application.Users.Commands.AddUserToRole;
using ShopShare.Contracts.Users;
using ShopShare.Domain.RoleAggregate.ValueObjects;
using ShopShare.Domain.UserAggregate.ValueObjects;

namespace ShopShare.API.Common.Mappers
{
    public class AddUserToRoleRequestToAddUserToRoleCommand : IMapper<AddUserToRoleRequest, AddUserToRoleCommand>
    {
        public AddUserToRoleCommand Map(AddUserToRoleRequest source)
        {
            return new AddUserToRoleCommand(
                UserId.Create(source.UserId),
                RoleId.Create(source.RoleId));
        }

        public IEnumerable<AddUserToRoleCommand> Map(IEnumerable<AddUserToRoleRequest> source)
        {
            return source.Select(Map);
        }
    }
}
