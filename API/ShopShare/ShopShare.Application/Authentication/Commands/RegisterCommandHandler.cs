using MediatR;
using ShopShare.Application.Services.Repositories;
using ShopShare.Domain.Common.Models;
using ShopShare.Domain.UserAggregate;
using ShopShare.Domain.UserAggregate.ValueObjects;

namespace ShopShare.Application.Authentication.Commands
{
    internal class RegisterCommandHandler : IRequestHandler<RegisterCommand, Result>
    {
        private readonly IUsersRepository _userRepository;

        public RegisterCommandHandler(IUsersRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<Result> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            //TODO: Validate correctnes of passed data
            if (_userRepository.Get(x => x.Email == request.Email && x.UserName == request.UserName) is not null)
            {
                return Result.Failure(
                    new(
                        "User.Exists",
                        "User with specified data already exists."));
            }

            var result = await _userRepository.AddAsync(
                request.UserName,
                request.Email,
                request.FirstName,
                request.LastName,
                request.Password);

            return result;
        }
    }
}
