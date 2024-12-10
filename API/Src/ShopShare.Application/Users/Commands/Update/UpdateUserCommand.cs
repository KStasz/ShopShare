using MediatR;
using ShopShare.Domain.Common.Models;
using ShopShare.Domain.UserAggregate;

namespace ShopShare.Application.Users.Commands.Update
{
    public record UpdateUserCommand(User User) 
        : IRequest<Result>;
}
