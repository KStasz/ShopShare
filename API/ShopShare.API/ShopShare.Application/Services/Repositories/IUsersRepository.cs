using ShopShare.Domain.Common.Models;
using ShopShare.Domain.UserAggregate;

namespace ShopShare.Application.Services.Repositories
{
    public interface IUsersRepository : IReadRepository<User>
    {
        Task<Result> AddAsync(string userName, string email, string firstName, string lastName, string password);
        Task<Result> DeleteAsync(User user);
        Task<Result> UpdateAsync(User user);
        Task<Result> VerifyPasswordAsync(User user, string password);
    }
}
