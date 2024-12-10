using MediatR;
using ShopShare.Application.Authentication.Models;
using ShopShare.Application.Services;
using ShopShare.Application.Services.Repositories;
using ShopShare.Domain.Common.Models;

namespace ShopShare.Application.Authentication.Query
{
    public class LoginQueryHandler : IRequestHandler<LoginQuery, Result<AuthenticationResult>>
    {
        private readonly IUsersRepository _userRepository;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;


        public LoginQueryHandler(
            IUsersRepository userRepository, 
            IJwtTokenGenerator jwtTokenGenerator)
        {
            _userRepository = userRepository;
            _jwtTokenGenerator = jwtTokenGenerator;
        }

        public async Task<Result<AuthenticationResult>> Handle(LoginQuery request, CancellationToken cancellationToken)
        {
            var user = _userRepository.Get(x => x.Email == request.Email);

            if (user is null)
            {
                return Result.Failure<AuthenticationResult>(new Error(
                    "Auth.Failed",
                    "Email or password is incorrect"));
            }

            var result = await _userRepository.VerifyPasswordAsync(user, request.Password);

            if (result.IsFailure)
            {
                return Result.Failure<AuthenticationResult>(result.Error!);
            }

            string token = _jwtTokenGenerator.GenerateToken(user);

            return Result.Success(
                new AuthenticationResult(
                    user, 
                    token));
        }
    }
}
