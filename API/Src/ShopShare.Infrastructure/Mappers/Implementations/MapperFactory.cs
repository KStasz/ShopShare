using Microsoft.Extensions.DependencyInjection;
using ShopShare.Application.Services.Mapper;

namespace ShopShare.Infrastructure.Mappers.Implementations
{
    public class MapperFactory : IMapperFactory
    {
        private readonly IServiceProvider _serviceProvider;
        public MapperFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public IMapper<TSource, TDestination> GetMapper<TSource, TDestination>()
        {
            var mapper = _serviceProvider.GetService<IMapper<TSource, TDestination>>();

            if(mapper is null)
            {
                throw new InvalidOperationException(
                    $"No mapper registered for {typeof(TSource).Name} to {typeof(TDestination).Name}.");
            }

            return mapper;
        }
    }
}
