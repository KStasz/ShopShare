using MediatR;
using ShopShare.Application.Services.Repositories;
using ShopShare.Domain.Common.Models;

namespace ShopShare.Application.Roles.Commands.Delete
{
    internal class DeleteRoleCommandHandler : IRequestHandler<DeleteRoleCommand, Result>
    {
        private readonly IRolesRepository _roleRepository;

        public DeleteRoleCommandHandler(IRolesRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        public async Task<Result> Handle(DeleteRoleCommand request, CancellationToken cancellationToken)
        {
            var result = await _roleRepository.DeleteAsync(request.Id, cancellationToken);

            return result;
        }
    }
}
