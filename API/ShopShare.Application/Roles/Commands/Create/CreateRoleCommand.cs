using MediatR;
using ShopShare.Domain.Common.Models;
using ShopShare.Domain.RoleAggregate;

namespace ShopShare.Application.Roles.Commands.Create
{
    public record CreateRoleCommand(string RoleName) : IRequest<Result<Role>>;
}
