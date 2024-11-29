using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ShopShare.Infrastructure.Model;

namespace ShopShare.Infrastructure.Persistance
{
    public class ShopShareDbContext
        : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>
    {
        public ShopShareDbContext(DbContextOptions<ShopShareDbContext> options)
            : base(options)
        {
        }
    }
}
