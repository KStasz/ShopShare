using Microsoft.AspNetCore.Identity;
using ShopShare.Application.Services.Mapper;
using ShopShare.Domain.RoleAggregate;
using ShopShare.Domain.RoleAggregate.ValueObjects;

namespace ShopShare.Infrastructure.Mappers
{
    internal class RoleAggregateFunctionToIdentityRoleFunctionMapper
        : IMapper<Func<Role, bool>, Func<IdentityRole<Guid>, bool>>
    {
        public Func<IdentityRole<Guid>, bool> Map(Func<Role, bool> source)
        {
            return identityRole =>
            {
                ArgumentException.ThrowIfNullOrWhiteSpace(identityRole.Name);

                return source(
                    Role.Create(
                        RoleId.Create(identityRole.Id),
                        identityRole.Name));
            };
        }

        public IEnumerable<Func<IdentityRole<Guid>, bool>> Map(IEnumerable<Func<Role, bool>> source)
        {
            return source.Select(Map);
        }
    }
}
