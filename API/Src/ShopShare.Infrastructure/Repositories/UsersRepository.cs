using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ShopShare.Application.Services.Mapper;
using ShopShare.Application.Services.Repositories;
using ShopShare.Domain.Common.Models;
using ShopShare.Domain.RoleAggregate;
using ShopShare.Domain.Snapshots;
using ShopShare.Domain.UserAggregate;
using ShopShare.Infrastructure.Errors;
using ShopShare.Infrastructure.Model;

namespace ShopShare.Infrastructure.Repositories
{
    public class UsersRepository : IUsersRepository
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IRolesRepository _rolesRepository;
        private readonly IMapper<Func<User, bool>, Func<ApplicationUser, bool>> _funcMapper;
        private readonly IMapper<ApplicationUser, UserSnapshot> _userSnapshotMapper;

        public UsersRepository(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IRolesRepository rolesRepository,
            IMapperFactory mapperFactory)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _rolesRepository = rolesRepository;
            _funcMapper = mapperFactory.GetMapper<Func<User, bool>, Func<ApplicationUser, bool>>();
            _userSnapshotMapper = mapperFactory.GetMapper<ApplicationUser, UserSnapshot>();
        }

        public async Task<Result> AddAsync(string userName, string email, string firstName, string lastName, string password)
        {
            var appUser = new ApplicationUser()
            {
                UserName = userName,
                Email = email,
                FirstName = firstName,
                LastName = lastName,
            };

            var result = await _userManager.CreateAsync(appUser, password);

            if (!result.Succeeded)
            {
                return Result.Failure(
                    new Error(result.Errors.Select(x => x.Code),
                    result.Errors.Select(x => x.Description)));
            }

            return Result.Success();
        }

        public async Task<Result> DeleteAsync(User model)
        {
            var user = await _userManager.FindByIdAsync(model.Id.Value.ToString());

            if (user is null)
            {
                return Result.Failure(InfrastructureErrors.User.UserDoesntExist);
            }

            var result = await _userManager.DeleteAsync(user);

            if (!result.Succeeded)
            {
                return Result.Failure(
                    new Error(
                        result.Errors.Select(x => x.Code),
                        result.Errors.Select(x => x.Description)));
            }

            return Result.Success();
        }

        public User? Get(Func<User, bool> predicate)
        {
            var appUserPredicate = _funcMapper.Map(predicate);
            var appUser = _userManager.Users
                .Include(x => x.UserRoles)
                .FirstOrDefault(appUserPredicate);

            if (appUser is null)
            {
                return null;
            }

            return User.FromShapshot(
                _userSnapshotMapper.Map(appUser));
        }

        public IEnumerable<User> GetAll(Func<User, bool> predicate)
        {
            var appUserPredicate = _funcMapper.Map(predicate);
            var appUsers = _userManager.Users
                .Include(x => x.UserRoles)
                .Where(appUserPredicate)
                .ToList();
            
            return appUsers.Select(
                x => User.FromShapshot(
                    _userSnapshotMapper.Map(x)));
        }

        public async Task<Result> UpdateAsync(User model)
        {
            var user = _userManager.Users
                .FirstOrDefault(x => x.Id == model.Id.Value);

            if (user is null)
            {
                return Result.Failure(InfrastructureErrors.User.UserDoesntExist);
            }

            UpdateUserProperties(model, user);

            var updatingRolesResult = await UpdateUserRoles(
                user,
                _rolesRepository.GetAll(x => model.UserRoles.Contains(x.Id)));

            if (!updatingRolesResult.Succeeded)
            {
                return Result.Failure(
                    new Error(
                        updatingRolesResult.Errors.Select(x => x.Code),
                        updatingRolesResult.Errors.Select(x => x.Description)));
            }

            var result = await _userManager.UpdateAsync(user);

            if (!result.Succeeded)
            {
                return Result.Failure(
                    new Error(
                        result.Errors.Select(x => x.Code),
                        result.Errors.Select(x => x.Description)));
            }

            return Result.Success();
        }

        private async Task<IdentityResult> UpdateUserRoles(
            ApplicationUser user,
            IEnumerable<Role> roles)
        {
            var currentUserRoles = await _userManager.GetRolesAsync(user);
            var removingResult = await _userManager.RemoveFromRolesAsync(
                user, 
                currentUserRoles);

            if (removingResult.Succeeded)
            {
                return await _userManager.AddToRolesAsync(
                    user, 
                    roles.Select(x => x.Name));
            }

            return removingResult;
        }

        private static void UpdateUserProperties(User model, ApplicationUser user)
        {
            user.UserName = model.UserName;
            user.Email = model.Email;
            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
        }

        public async Task<Result> VerifyPasswordAsync(User user, string password)
        {
            var appUser = _userManager.Users.FirstOrDefault(x => x.Id == user.Id.Value);

            if (appUser is null)
            {
                return Result.Failure(InfrastructureErrors.Authentication.SignInFailed);
            }

            var result = await _signInManager.CheckPasswordSignInAsync(
                appUser,
                password,
                false);

            if (!result.Succeeded)
            {
                return Result.Failure(InfrastructureErrors.Authentication.SignInFailed);
            }

            return Result.Success();
        }
    }
}
