using MediatR;
using ShopShare.Domain.Common.Models;
using ShopShare.Domain.UserAggregate.ValueObjects;

namespace ShopShare.Application.Users.Commands.Delete
{
    public record DeleteUserCommand(UserId Id) 
        : IRequest<Result>;
}
