using MediatR;
using ShopShare.Domain.Common.Models;
using ShopShare.Domain.RoleAggregate.ValueObjects;
using ShopShare.Domain.UserAggregate.ValueObjects;

namespace ShopShare.Application.Users.Commands.AddUserToRole
{
    public record AddUserToRoleCommand(
        UserId UserId,
        RoleId RoleId) 
        : IRequest<Result>;
}
