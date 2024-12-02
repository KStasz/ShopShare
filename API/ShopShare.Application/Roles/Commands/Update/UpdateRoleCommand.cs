using MediatR;
using ShopShare.Domain.Common.Models;
using ShopShare.Domain.RoleAggregate;

namespace ShopShare.Application.Roles.Commands.Update
{
    public record UpdateRoleCommand(Role Role) : IRequest<Result>;
}
