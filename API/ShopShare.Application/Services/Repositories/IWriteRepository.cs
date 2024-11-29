namespace ShopShare.Application.Services.Repositories
{
    public interface IWriteRepository<TModel>
    {
        Task AddAsync(TModel model);
        Task UpdateAsync(TModel model);
        Task DeleteAsync(TModel model);
    }
}
