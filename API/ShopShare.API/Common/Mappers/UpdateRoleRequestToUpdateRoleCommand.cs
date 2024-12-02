using ShopShare.Application.Roles.Commands.Update;
using ShopShare.Application.Services.Mapper;
using ShopShare.Contracts.Roles;
using ShopShare.Domain.RoleAggregate;
using ShopShare.Domain.RoleAggregate.ValueObjects;

namespace ShopShare.API.Common.Mappers
{
    public class UpdateRoleRequestToUpdateRoleCommand : IMapper<UpdateRoleRequest, UpdateRoleCommand>
    {
        public UpdateRoleCommand Map(UpdateRoleRequest source)
        {
            return new UpdateRoleCommand(
                Role.Create(
                    RoleId.Create(source.Id),
                    source.Name));
        }

        public IEnumerable<UpdateRoleCommand> Map(IEnumerable<UpdateRoleRequest> source)
        {
            return source.Select(Map);
        }
    }
}
