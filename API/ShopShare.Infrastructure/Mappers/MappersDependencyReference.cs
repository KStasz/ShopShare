using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using ShopShare.Application.Services.Mapper;
using ShopShare.Domain.RoleAggregate;
using ShopShare.Domain.UserAggregate;
using ShopShare.Infrastructure.Model;

namespace ShopShare.Infrastructure.Mappers
{
    internal static class MappersDependencyReference
    {
        public static IServiceCollection RegisterMappers(this IServiceCollection services)
        {
            services.AddSingleton<IMapper<ApplicationUser, User>, ApplicationUserToUserAggregateMapper>();
            services.AddSingleton<IMapper<Func<User, bool>, Func<ApplicationUser, bool>>, UserAggregateFunctionToApplicationUserFunctionMapper>();
            services.AddSingleton<IMapper<Func<Role, bool>, Func<IdentityRole<Guid>, bool>>, RoleAggregateFunctionToIdentityRoleFunctionMapper>();
            services.AddSingleton<IMapper<IdentityRole<Guid>, Role>, IdentityRoleToRoleMapper>();

            return services;
        }
    }
}
