using MediatR;
using ShopShare.Domain.Common.Models;
using ShopShare.Domain.ShoppingListAggregate.ValueObjects;

namespace ShopShare.Application.ListItem.Queries
{
    public record GetListItemsQuery(
        ShoppingListId ShoppingListId)
        : IRequest<Result<IEnumerable<Domain.ShoppingListAggregate.Entities.ListItem>>>;
}
