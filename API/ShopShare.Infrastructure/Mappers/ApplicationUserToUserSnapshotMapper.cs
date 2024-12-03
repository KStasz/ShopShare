using ShopShare.Application.Services.Mapper;
using ShopShare.Domain.Snapshots;
using ShopShare.Infrastructure.Model;

namespace ShopShare.Infrastructure.Mappers
{
    public class ApplicationUserToUserSnapshotMapper : IMapper<ApplicationUser, UserSnapshot>
    {
        public UserSnapshot Map(ApplicationUser source)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(source.UserName);
            ArgumentException.ThrowIfNullOrWhiteSpace(source.Email);

            return new UserSnapshot(
                source.Id,
                source.UserName,
                source.Email,
                source.FirstName,
                source.LastName,
                source.CreationDate,
                source.UserRoles.Select(x => x.RoleId));
        }

        public IEnumerable<UserSnapshot> Map(IEnumerable<ApplicationUser> source)
        {
            return source.Select(Map);
        }
    }
}
