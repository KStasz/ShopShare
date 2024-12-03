using MediatR;
using ShopShare.Application.Errors;
using ShopShare.Application.Services.Repositories;
using ShopShare.Domain.Common.Models;
using ShopShare.Domain.UserAggregate;

namespace ShopShare.Application.Users.Queries.GetOne
{
    public class GetUserQueryHandler : IRequestHandler<GetUserQuery, Result<User>>
    {
        private readonly IUsersRepository _usersRepository;
        public GetUserQueryHandler(IUsersRepository usersRepository)
        {
            _usersRepository = usersRepository;
        }

        public async Task<Result<User>> Handle(GetUserQuery request, CancellationToken cancellationToken)
        {
            await Task.CompletedTask;
            var result = _usersRepository.Get(x => x.Id == request.Id);

            if(result is null)
            {
                return Result.Failure<User>(
                    ApplicationErrors.User.UserNotFound);
            }

            return Result.Success(result);
        }
    }
}
