using Microsoft.AspNetCore.Identity;

namespace ShopShare.Infrastructure.Model
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public DateTime CreationDate { get; set; } = new DateTime();

        public IReadOnlyList<IdentityUserRole<Guid>> UserRoles { get; set; } = null!;
    }
}
