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
        }

        public static class User
        {
            public static readonly Error UserNotFound = new(
            "User.NotFound",
            "Specified user not found.");
        }
    }
}
