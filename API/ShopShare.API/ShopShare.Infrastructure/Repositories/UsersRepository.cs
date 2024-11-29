using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ShopShare.Application.Services.Mapper;
using ShopShare.Application.Services.Repositories;
using ShopShare.Domain.Common.Models;
using ShopShare.Domain.UserAggregate;
using ShopShare.Infrastructure.Errors;
using ShopShare.Infrastructure.Model;

namespace ShopShare.Infrastructure.Repositories
{
    public class UsersRepository : IUsersRepository
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IMapper<ApplicationUser, User> _userAggregateMapper;
        private readonly IMapper<Func<User, bool>, Func<ApplicationUser, bool>> _funcMapper;

        public UsersRepository(
            UserManager<ApplicationUser> userManager,
            IMapper<ApplicationUser, User> userAggregateMapper,
            IMapper<Func<User, bool>, Func<ApplicationUser, bool>> funcMapper,
            SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _userAggregateMapper = userAggregateMapper;
            _funcMapper = funcMapper;
            _signInManager = signInManager;
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
            var appUser = _userManager.Users.AsNoTracking().FirstOrDefault(appUserPredicate);

            if (appUser is null)
            {
                return null;
            }

            return _userAggregateMapper.Map(appUser);
        }

        public IEnumerable<User> GetAll(Func<User, bool> predicate)
        {
            var appUserPredicate = _funcMapper.Map(predicate);
            var appUser = _userManager.Users.Where(appUserPredicate);

            return _userAggregateMapper.Map(appUser);
        }

        public async Task<Result> UpdateAsync(User model)
        {
            var user = await _userManager.FindByIdAsync(model.Id.Value.ToString());

            if (user is null)
            {
                return Result.Failure(InfrastructureErrors.User.UserDoesntExist);
            }

            user.UserName = model.UserName;
            user.Email = model.Email;
            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
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

        public async Task<Result> VerifyPasswordAsync(User user, string password)
        {
            var appUser = _userManager.Users.FirstOrDefault(x => x.Id == user.Id.Value);

            if(appUser is null)
            {
                return Result.Failure(InfrastructureErrors.Authentication.SignInFailed);
            }

            var result = await _signInManager.CheckPasswordSignInAsync(
                appUser, 
                password, 
                false);

            if(!result.Succeeded)
            {
                return Result.Failure(InfrastructureErrors.Authentication.SignInFailed);
            }

            return Result.Success();
        }
    }
}
