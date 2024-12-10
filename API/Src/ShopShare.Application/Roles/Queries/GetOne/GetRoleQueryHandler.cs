using MediatR;
using ShopShare.Application.Errors;
using ShopShare.Application.Services.Repositories;
using ShopShare.Domain.Common.Models;
using ShopShare.Domain.RoleAggregate;
using ShopShare.Domain.RoleAggregate.ValueObjects;

namespace ShopShare.Application.Roles.Queries.GetOne
{
    public class GetRoleQueryHandler : IRequestHandler<GetRoleQuery, Result<Role>>
    {
        private readonly IRolesRepository _rolesRepository;

        public GetRoleQueryHandler(IRolesRepository rolesRepository)
        {
            _rolesRepository = rolesRepository;
        }

        public async Task<Result<Role>> Handle(GetRoleQuery request, CancellationToken cancellationToken)
        {
            await Task.CompletedTask;
            var result = _rolesRepository.Get(
                x => x.Id == RoleId.Create(request.Id));

            if (result is null)
            {
                return Result.Failure<Role>(
                    ApplicationErrors.Role.RoleNotFound);
            }

            return Result.Success(result);
        }
    }
}
