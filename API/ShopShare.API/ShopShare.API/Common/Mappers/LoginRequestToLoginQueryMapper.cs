using ShopShare.Application.Authentication.Query;
using ShopShare.Application.Services.Mapper;
using ShopShare.Contracts.Authentication;

namespace ShopShare.API.Common.Mappers
{
    public class LoginRequestToLoginQueryMapper : IMapper<LoginRequest, LoginQuery>
    {
        public LoginQuery Map(LoginRequest source)
        {
            return new LoginQuery(
                source.Email,
                source.Password);
        }

        public IEnumerable<LoginQuery> Map(IEnumerable<LoginRequest> source)
        {
            return source.Select(Map);
        }
    }
}
