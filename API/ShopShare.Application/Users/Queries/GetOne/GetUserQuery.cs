using MediatR;
using ShopShare.Domain.Common.Models;
using ShopShare.Domain.UserAggregate;
using ShopShare.Domain.UserAggregate.ValueObjects;

namespace ShopShare.Application.Users.Queries.GetOne
{
    public record GetUserQuery(UserId Id) 
        : IRequest<Result<User>>;
}
