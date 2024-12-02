using MediatR;
using ShopShare.Application.Errors;
using ShopShare.Application.Services.Repositories;
using ShopShare.Domain.Common.Models;
using ShopShare.Domain.RoleAggregate;

namespace ShopShare.Application.Roles.Queries.GetAll
{
    public class GetRolesQueryHandler : IRequestHandler<GetRolesQuery, Result<IEnumerable<Role>>>
    {
        private readonly IRolesRepository _rolesRepository;

        public GetRolesQueryHandler(IRolesRepository rolesRepository)
        {
            _rolesRepository = rolesRepository;
        }

        public async Task<Result<IEnumerable<Role>>> Handle(GetRolesQuery request, CancellationToken cancellationToken)
        {
            await Task.CompletedTask;
            IEnumerable<Role> result = _rolesRepository.GetAll(x => true);

            if (result is null || !result.Any())
            {
                return Result.Failure<IEnumerable<Role>>(
                    ApplicationErrors.Role.RolesNotFound);
            }

            return Result.Success(result);
        }
    }
}
