using MediatR;
using ShopShare.Domain.Common.Models;
using ShopShare.Domain.RoleAggregate;

namespace ShopShare.Application.Roles.Queries.GetAll
{
    public class GetRolesQuery() 
        : IRequest<Result<IEnumerable<Role>>>;
}
