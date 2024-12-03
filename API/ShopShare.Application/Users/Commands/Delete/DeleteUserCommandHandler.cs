using MediatR;
using ShopShare.Application.Errors;
using ShopShare.Application.Services.Repositories;
using ShopShare.Domain.Common.Models;

namespace ShopShare.Application.Users.Commands.Delete
{
    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, Result>
    {
        private readonly IUsersRepository _usersRepository;
        public DeleteUserCommandHandler(IUsersRepository usersRepository)
        {
            _usersRepository = usersRepository;
        }

        public async Task<Result> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var user = _usersRepository.Get(x => x.Id == request.Id);

            if (user is null)
            {
                return Result.Failure(ApplicationErrors.User.UserNotFound);
            }

            var result = await _usersRepository.DeleteAsync(user);

            return result;
        }
    }
}
