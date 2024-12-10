using MediatR;
using ShopShare.Application.Services.Repositories;
using ShopShare.Domain.Common.Models;
using ShopShare.Domain.UserAggregate;

namespace ShopShare.Application.Users.Queries.GetAll
{
    public class GetUsersQueryHandler : IRequestHandler<GetUsersQuery, Result<IEnumerable<User>>>
    {
        private readonly IUsersRepository _usersRepository;
        public GetUsersQueryHandler(IUsersRepository usersRepository)
        {
            _usersRepository = usersRepository;
        }

        public async Task<Result<IEnumerable<User>>> Handle(
            GetUsersQuery request, 
            CancellationToken cancellationToken)
        {
            await Task.CompletedTask;

            return Result.Success(_usersRepository.GetAll(x => true));
        }
    }
}
