using ShopShare.API.Common;

namespace ShopShare.API
{
    public static class DependecyReference
    {
        public static IServiceCollection AddPresentation(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddControllers();
            serviceCollection.AddEndpointsApiExplorer();
            serviceCollection.AddSwaggerGen();
            serviceCollection.AddMappers();

            return serviceCollection;
        }
    }
}
