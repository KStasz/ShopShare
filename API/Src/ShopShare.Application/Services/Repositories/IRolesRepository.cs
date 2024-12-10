using ShopShare.Domain.Common.Models;
using ShopShare.Domain.RoleAggregate;

namespace ShopShare.Application.Services.Repositories
{
    public interface IRolesRepository : IReadRepository<Role>
    {
        Task<Result<Role>> AddAsync(string name, CancellationToken cancellationToken = default);
        Task<Result> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
        Task<Result> UpdateAsync(Role role, CancellationToken cancellationToken = default);
    }
}
