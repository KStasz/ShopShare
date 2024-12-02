using ShopShare.Application.Services.Mapper;
using ShopShare.Contracts.Roles;
using ShopShare.Domain.RoleAggregate;

namespace ShopShare.API.Common.Mappers
{
    public class RoleToRoleResponseMapper : IMapper<Role, RoleResponse>
    {
        public RoleResponse Map(Role source)
        {
            return new RoleResponse(
                source.Id.Value,
                source.Name);
        }

        public IEnumerable<RoleResponse> Map(IEnumerable<Role> source)
        {
            return source.Select(Map);
        }
    }
}
