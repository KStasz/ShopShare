using Microsoft.AspNetCore.Identity;
using ShopShare.Application.Services.Mapper;
using ShopShare.Application.Services.Repositories;
using ShopShare.Domain.Common.Models;
using ShopShare.Domain.RoleAggregate;
using ShopShare.Infrastructure.Errors;

namespace ShopShare.Infrastructure.Repositories
{
    internal class RolesRepository : IRolesRepository
    {
        private readonly RoleManager<IdentityRole<Guid>> _roleManager;
        private readonly IMapper<Func<Role, bool>, Func<IdentityRole<Guid>, bool>> _roleFunctionMapper;
        private readonly IMapper<IdentityRole<Guid>, Role> _roleMapper;

        public RolesRepository(
            RoleManager<IdentityRole<Guid>> roleManager,
            IMapperFactory mapperFactory)
        {
            _roleManager = roleManager;
            _roleFunctionMapper = mapperFactory.GetMapper<Func<Role, bool>, Func<IdentityRole<Guid>, bool>>();
            _roleMapper = mapperFactory.GetMapper<IdentityRole<Guid>, Role>();
        }

        public async Task<Result<Role>> AddAsync(string name, CancellationToken cancellationToken = default)
        {
            var identityRole = new IdentityRole<Guid>(name);

            var result = await _roleManager.CreateAsync(identityRole);

            if (!result.Succeeded)
            {
                return Result.Failure<Role>(new Error(
                    result.Errors.Select(x => x.Code),
                    result.Errors.Select(x => x.Description)));
            }

            return Result.Success(
                _roleMapper.Map(
                    identityRole));
        }

        public async Task<Result> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var identityRole = _roleManager.Roles.FirstOrDefault(x => x.Id == id);

            if (identityRole is null)
            {
                return Result.Failure(InfrastructureErrors.Role.RoleDoesntExisit);
            }

            var result = await _roleManager.DeleteAsync(identityRole);

            if(!result.Succeeded)
            {
                return Result.Failure(
                    new Error(
                        result.Errors.Select(x => x.Code),
                        result.Errors.Select(x => x.Description)));
            }

            return Result.Success();
        }

        public Role? Get(Func<Role, bool> predicate)
        {
            var identityRolePredicate = _roleFunctionMapper.Map(predicate);
            var identityRole = _roleManager.Roles.FirstOrDefault(identityRolePredicate);

            if(identityRole is null)
            {
                return null;
            }

            return _roleMapper.Map(identityRole);
        }

        public IEnumerable<Role> GetAll(Func<Role, bool> predicate)
        {
            var identityRolePredicate = _roleFunctionMapper.Map(predicate);
            var identityRoleCollection = _roleManager.Roles
                .Where(identityRolePredicate)
                .ToList();

            return _roleMapper.Map(identityRoleCollection);
        }

        public async Task<Result> UpdateAsync(Role role, CancellationToken cancellationToken = default)
        {
            var identityRole = _roleManager.Roles.FirstOrDefault(x => x.Id == role.Id.Value);
            
            if(identityRole is null)
            {
                return Result.Failure(InfrastructureErrors.Role.RoleDoesntExisit);
            }

            identityRole.Name = role.Name;
            var result = await _roleManager.UpdateAsync(identityRole);

            if(!result.Succeeded)
            {
                return Result.Failure(
                    new Error(
                        result.Errors.Select(x => x.Code),
                        result.Errors.Select(x => x.Description)));
            }

            return Result.Success();
        }
    }
}
