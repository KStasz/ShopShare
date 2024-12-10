using MediatR;
using ShopShare.Domain.Common.Models;
using ShopShare.Domain.RoleAggregate;

namespace ShopShare.Application.Roles.Queries.GetOne
{
    public record GetRoleQuery(Guid Id) 
        : IRequest<Result<Role>>;
}
