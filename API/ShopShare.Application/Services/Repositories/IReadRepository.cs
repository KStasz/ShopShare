using ShopShare.Domain.Common.Models;

namespace ShopShare.Application.Services.Repositories
{
    public interface IReadRepository<TModel>
    {
        TModel? Get(Func<TModel, bool> predicate);
        IEnumerable<TModel> GetAll(Func<TModel, bool> predicate);
    }
}
