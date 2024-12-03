using ShopShare.Application.Authentication.Models;
using ShopShare.Application.Services.Mapper;
using ShopShare.Contracts.Authentication;
using ShopShare.Contracts.Users;
using ShopShare.Domain.UserAggregate;

namespace ShopShare.API.Common.Mappers
{
    public class AuthenticationResultToAuthenticationResponseMapper
        : IMapper<AuthenticationResult, AuthenticationResponse>
    {
        private readonly IMapper<User, UserResponse> _userResponse;
        public AuthenticationResultToAuthenticationResponseMapper(
            IMapper<User, UserResponse> userResponse)
        {
            _userResponse = userResponse;
        }

        public AuthenticationResponse Map(AuthenticationResult source)
        {
            return new AuthenticationResponse(
                _userResponse.Map(source.user),
                source.token);
        }

        public IEnumerable<AuthenticationResponse> Map(IEnumerable<AuthenticationResult> source)
        {
            return source.Select(Map);
        }
    }
}
