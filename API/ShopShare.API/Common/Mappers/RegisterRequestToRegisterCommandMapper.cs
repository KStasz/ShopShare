using ShopShare.Application.Authentication.Commands;
using ShopShare.Application.Services.Mapper;
using ShopShare.Contracts.Authentication;

namespace ShopShare.API.Common.Mappers
{
    public class RegisterRequestToRegisterCommandMapper : IMapper<RegisterRequest, RegisterCommand>
    {
        public RegisterCommand Map(RegisterRequest source)
        {
            return new RegisterCommand(
                source.UserName,
                source.Email,
                source.FirstName,
                source.LastName,
                source.Password);
        }

        public IEnumerable<RegisterCommand> Map(IEnumerable<RegisterRequest> source)
        {
            return source.Select(Map);
        }
    }
}
