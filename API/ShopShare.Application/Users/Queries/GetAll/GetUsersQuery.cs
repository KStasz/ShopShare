using MediatR;
using ShopShare.Domain.Common.Models;
using ShopShare.Domain.UserAggregate;

namespace ShopShare.Application.Users.Queries.GetAll
{
    public record GetUsersQuery() 
        : IRequest<Result<IEnumerable<User>>>;
}
