using ShopShare.API.Common;
using System.Reflection;

namespace ShopShare.API
{
    public static class DependecyReference
    {
        public static IServiceCollection AddPresentation(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddControllers();
            serviceCollection.AddEndpointsApiExplorer();
            serviceCollection.AddSwaggerGen();
            serviceCollection.AddMappers(Assembly.GetExecutingAssembly());

            return serviceCollection;
        }
    }
}
