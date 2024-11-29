using ShopShare.API.Common.Mappers;
using ShopShare.Application.Authentication.Commands;
using ShopShare.Application.Authentication.Query;
using ShopShare.Application.Services.Mapper;
using ShopShare.Contracts.Authentication;

namespace ShopShare.API.Common
{
    public static class PresentationMappersDependencyReference
    {
        public static IServiceCollection AddMappers(this IServiceCollection services)
        {
            services.AddSingleton<IMapper<RegisterRequest, RegisterCommand>, RegisterRequestToRegisterCommandMapper>();
            services.AddSingleton<IMapper<LoginRequest, LoginQuery>, LoginRequestToLoginQueryMapper>();

            return services;
        }
    }
}
