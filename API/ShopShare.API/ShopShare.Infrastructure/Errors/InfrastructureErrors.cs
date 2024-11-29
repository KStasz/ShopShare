using ShopShare.Domain.Common.Models;

namespace ShopShare.Infrastructure.Errors
{
    public class InfrastructureErrors
    {
        public static class User
        {
            public static readonly Error UserDoesntExist = new(
                "User.DoesNotExist",
                "Specified user doesnt exist");
        }

        public static class Authentication
        {
            public static readonly Error SignInFailed = new(
                "Auth.Failed",
                "Email or password is incorrect");
        }
    }
}
