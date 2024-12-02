using ShopShare.Application.Roles.Commands.Create;
using ShopShare.Application.Services.Mapper;
using ShopShare.Contracts.Roles;

namespace ShopShare.API.Common.Mappers
{
    public class CreateRoleRequestToCreateRoleMapper : IMapper<CreateRoleRequest, CreateRoleCommand>
    {
        public CreateRoleCommand Map(CreateRoleRequest source)
        {
            return new CreateRoleCommand(source.RoleName);
        }

        public IEnumerable<CreateRoleCommand> Map(IEnumerable<CreateRoleRequest> source)
        {
            return source.Select(Map);
        }
    }
}
