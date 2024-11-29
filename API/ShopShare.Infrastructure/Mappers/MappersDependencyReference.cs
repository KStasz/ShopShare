using Microsoft.Extensions.DependencyInjection;
using ShopShare.Application.Services.Mapper;
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

            return services;
        }
    }
}
