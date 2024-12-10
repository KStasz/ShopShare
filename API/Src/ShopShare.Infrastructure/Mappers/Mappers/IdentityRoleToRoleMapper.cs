using Microsoft.AspNetCore.Identity;
using ShopShare.Application.Services.Mapper;
using ShopShare.Domain.RoleAggregate;
using ShopShare.Domain.RoleAggregate.ValueObjects;

namespace ShopShare.Infrastructure.Mappers.Mappers
{
    internal class IdentityRoleToRoleMapper : IMapper<IdentityRole<Guid>, Role>
    {
        public Role Map(IdentityRole<Guid> source)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(source.Name);

            return Role.Create(
                RoleId.Create(source.Id),
                source.Name);
        }

        public IEnumerable<Role> Map(IEnumerable<IdentityRole<Guid>> source)
        {
            return source.Select(Map);
        }
    }
}
