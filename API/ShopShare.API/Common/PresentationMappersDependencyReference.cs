using ShopShare.API.Common.Mappers;
using ShopShare.Application.Authentication.Commands;
using ShopShare.Application.Authentication.Query;
using ShopShare.Application.Roles.Commands.Create;
using ShopShare.Application.Roles.Commands.Update;
using ShopShare.Application.Services.Mapper;
using ShopShare.Contracts.Authentication;
using ShopShare.Contracts.Roles;
using ShopShare.Domain.RoleAggregate;

namespace ShopShare.API.Common
{
    public static class PresentationMappersDependencyReference
    {
        public static IServiceCollection AddMappers(this IServiceCollection services)
        {
            services.AddSingleton<IMapper<RegisterRequest, RegisterCommand>, RegisterRequestToRegisterCommandMapper>();
            services.AddSingleton<IMapper<LoginRequest, LoginQuery>, LoginRequestToLoginQueryMapper>();
            services.AddSingleton<IMapper<LoginRequest, LoginQuery>, LoginRequestToLoginQueryMapper>();
            services.AddSingleton<IMapper<CreateRoleRequest, CreateRoleCommand>, CreateRoleRequestToCreateRoleMapper>();
            services.AddSingleton<IMapper<UpdateRoleRequest, UpdateRoleCommand>, UpdateRoleRequestToUpdateRoleCommand>();
            services.AddSingleton<IMapper<Role, RoleResponse>, RoleToRoleResponseMapper>();

            return services;
        }
    }
}
