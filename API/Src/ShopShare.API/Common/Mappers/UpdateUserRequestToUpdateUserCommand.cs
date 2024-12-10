using ShopShare.Application.Services.Mapper;
using ShopShare.Application.Users.Commands.Update;
using ShopShare.Contracts.Users;
using ShopShare.Domain.Snapshots;
using ShopShare.Domain.UserAggregate;

namespace ShopShare.API.Common.Mappers
{
    public class UpdateUserRequestToUpdateUserCommand : IMapper<UpdateUserRequest, UpdateUserCommand>
    {
        public UpdateUserCommand Map(UpdateUserRequest source)
        {
            var userSnapshot = new UserSnapshot(
                source.Id,
                source.UserName, 
                source.Email,
                source.FirstName,
                source.LastName,
                source.CreationDate, 
                source.RoleIds);

            return new UpdateUserCommand(
                User.FromShapshot(userSnapshot));
        }

        public IEnumerable<UpdateUserCommand> Map(IEnumerable<UpdateUserRequest> source)
        {
            return source.Select(Map);
        }
    }
}
