namespace ShopShare.Application.Services.Mapper
{
    public interface IMapperFactory
    {
        IMapper<TSource, TDestination> GetMapper<TSource, TDestination>();
    }
}
