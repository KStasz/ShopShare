using MediatR;
using ShopShare.Domain.Common.Models;

namespace ShopShare.Application.Roles.Commands.Delete
{
    public record DeleteRoleCommand(Guid Id) : IRequest<Result>;
}
