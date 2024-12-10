using MediatR;
using ShopShare.Application.Services.Repositories;
using ShopShare.Domain.Common.Models;

namespace ShopShare.Application.Roles.Commands.Update
{
    internal class UpdateRoleCommandHandler : IRequestHandler<UpdateRoleCommand, Result>
    {
        private readonly IRolesRepository _rolesRepository;

        public UpdateRoleCommandHandler(IRolesRepository rolesRepository)
        {
            _rolesRepository = rolesRepository;
        }

        public async Task<Result> Handle(UpdateRoleCommand request, CancellationToken cancellationToken)
        {
            var result = await _rolesRepository.UpdateAsync(request.Role, cancellationToken);

            return result;
        }
    }
}
