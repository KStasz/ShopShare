using MediatR;
using ShopShare.Application.Services.Repositories;
using ShopShare.Domain.Common.Models;
using ShopShare.Domain.RoleAggregate;

namespace ShopShare.Application.Roles.Commands.Create
{
    public class CreateRoleCommandHandler : IRequestHandler<CreateRoleCommand, Result<Role>>
    {
        private readonly IRolesRepository _rolesRepository;

        public CreateRoleCommandHandler(IRolesRepository rolesRepository)
        {
            _rolesRepository = rolesRepository;
        }

        public async Task<Result<Role>> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
        {
            if (_rolesRepository.Get(x => x.Name == request.RoleName) is not null)
            {
                return Result.Failure<Role>(
                    new Error(
                        "Role.AlreadyExists",
                        "Role with specified data already exists."));
            }

            var result = await _rolesRepository.AddAsync(request.RoleName, cancellationToken);

            return result;
        }
    }
}
