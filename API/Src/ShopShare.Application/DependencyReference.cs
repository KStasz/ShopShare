using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using ShopShare.Application.Common.Validation;
using System.Reflection;

namespace ShopShare.Application
{
    public static class DependencyReference
    {
        public static IServiceCollection AddApplication(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddMediatR(
                config => config.RegisterServicesFromAssemblies(
                    AppDomain.CurrentDomain.GetAssemblies()));
            serviceCollection.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            serviceCollection.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            return serviceCollection;
        }
    }
}
