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
            services.AddSingleton<IMapper<User, UserResponse>, UserAggregateToUserResponseMapper>();
            services.AddSingleton<IMapper<AuthenticationResult, AuthenticationResponse>, AuthenticationResultToAuthenticationResponseMapper>();
            services.AddSingleton<IMapper<UpdateUserRequest, UpdateUserCommand>, UpdateUserRequestToUpdateUserCommand>();
            services.AddSingleton<IMapper<UpdateUserRequest, UpdateUserCommand>, UpdateUserRequestToUpdateUserCommand>();
            services.AddSingleton<IMapper<AddUserToRoleRequest, AddUserToRoleCommand>, AddUserToRoleRequestToAddUserToRoleCommand>();

            return services;
        }
    }
}
