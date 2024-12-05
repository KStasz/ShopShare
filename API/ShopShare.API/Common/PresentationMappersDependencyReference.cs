using ShopShare.API.Common.Implementations;
using ShopShare.API.Common.Mappers;
using ShopShare.Application.Authentication.Commands;
using ShopShare.Application.Authentication.Models;
using ShopShare.Application.Authentication.Query;
using ShopShare.Application.Roles.Commands.Create;
using ShopShare.Application.Roles.Commands.Update;
using ShopShare.Application.Services.Mapper;
using ShopShare.Application.Users.Commands.AddUserToRole;
using ShopShare.Application.Users.Commands.Update;
using ShopShare.Contracts.Authentication;
using ShopShare.Contracts.Roles;
using ShopShare.Contracts.Users;
using ShopShare.Domain.RoleAggregate;
using ShopShare.Domain.UserAggregate;
using System.Reflection;

namespace ShopShare.API.Common
{
    public static class PresentationMappersDependencyReference
    {
        public static IServiceCollection AddMappers(this IServiceCollection services, Assembly assembly)
        {
            var mapperTypes = assembly.GetTypes()
                .Where(x => !x.IsAbstract && !x.IsInterface)
                .SelectMany(x => x.GetInterfaces()
                    .Where(y => y.IsGenericType && y.GetGenericTypeDefinition() == typeof(IMapper<,>))
                    .Select(y => new { Implementation = x, Interface = y }));

            foreach(var mapper in mapperTypes)
            {
                services.AddSingleton(mapper.Interface, mapper.Implementation);
            }

            services.AddSingleton<IMapperFactory, MapperFactory>();

            return services;
        }
    }
}
