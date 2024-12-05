using Microsoft.Extensions.DependencyInjection;
using ShopShare.Application.Services.Mapper;
using ShopShare.Infrastructure.Mappers.Implementations;
using System.Reflection;

namespace ShopShare.Infrastructure.Mappers
{
    internal static class MappersDependencyReference
    {
        public static IServiceCollection RegisterMappers(this IServiceCollection services, Assembly assembly)
        {
            var mapperTypes = assembly.GetTypes()
                .Where(x => !x.IsAbstract && !x.IsInterface)
                .SelectMany(x => x.GetInterfaces()
                    .Where(y => y.IsGenericType && y.GetGenericTypeDefinition() == typeof(IMapper<,>))
                    .Select(y => new { Implementation = x, Interface = y }));

            foreach (var mapper in mapperTypes)
            {
                services.AddSingleton(mapper.Interface, mapper.Implementation);
            }

            services.AddSingleton<IMapperFactory, MapperFactory>();

            return services;
        }
    }
}
