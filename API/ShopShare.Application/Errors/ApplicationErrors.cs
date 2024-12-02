using ShopShare.Domain.Common.Models;

namespace ShopShare.Application.Errors
{
    public class ApplicationErrors
    {
        public static class Role
        {
            public static readonly Error RoleNotFound = new(
            "Role.NotFound",
            "Specified role not found.");

            public static readonly Error RolesNotFound = new(
            "Role.NotFound",
            "Specified role not found.");
        }
    }
}
