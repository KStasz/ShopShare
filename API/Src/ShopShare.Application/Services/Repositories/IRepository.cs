using System.Linq.Expressions;

namespace ShopShare.Application.Services.Repositories
{
    public interface IRepository<TModel> 
        : IReadRepository<TModel>, IWriteRepository<TModel>
    {
    }
}
