using Microsoft.Extensions.DependencyInjection;

namespace ShopShare.Application
{
    public static class DependencyReference
    {
        public static IServiceCollection AddApplication(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddMediatR(
                config => config.RegisterServicesFromAssemblies(
                    AppDomain.CurrentDomain.GetAssemblies()));

            return serviceCollection;
        }
    }
}
