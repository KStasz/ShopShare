using MediatR;
using ShopShare.Application.Errors;
using ShopShare.Application.Services.Repositories;
using ShopShare.Domain.Common.Models;

namespace ShopShare.Application.Users.Commands.AddUserToRole
{
    public class AddUserToRoleCommandHandler : IRequestHandler<AddUserToRoleCommand, Result>
    {
        private readonly IUsersRepository _usersRepository;
        public AddUserToRoleCommandHandler(IUsersRepository usersRepository)
        {
            _usersRepository = usersRepository;
        }

        public async Task<Result> Handle(AddUserToRoleCommand request, CancellationToken cancellationToken)
        {
            var user = _usersRepository.Get(x => x.Id == request.UserId);

            if (user is null)
            {
                return Result.Failure(ApplicationErrors.User.UserNotFound);
            }

            user.AddRole(request.RoleId);
            var result = await _usersRepository.UpdateAsync(user);

            return result;
        }
    }
}
