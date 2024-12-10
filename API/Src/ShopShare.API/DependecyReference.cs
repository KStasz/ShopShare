using Microsoft.AspNetCore.ResponseCompression;
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
            serviceCollection.AddSignalR();

            serviceCollection.AddResponseCompression(opts =>
            {
                opts.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(
                    ["application/octet-stream"]);
            });

            return serviceCollection;
        }
    }
}
