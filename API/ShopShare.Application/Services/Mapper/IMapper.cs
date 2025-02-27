﻿namespace ShopShare.Application.Services.Mapper
{
    public interface IMapper<TSource, TDestination>
    {
        TDestination Map(TSource source);
        IEnumerable<TDestination> Map(IEnumerable<TSource> source);
    }
}
