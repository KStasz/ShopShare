using MediatR;
using ShopShare.Domain.Common.Models;
using ShopShare.Domain.ShoppingListAggregate.ValueObjects;

namespace ShopShare.Application.ListItem.Commands.Create
{
    public record CreateListItemCommand(
        ShoppingListId ShoppingListId,
        Domain.ShoppingListAggregate.Entities.ListItem ListItem) : IRequest<Result<Domain.ShoppingListAggregate.Entities.ListItem>>;
}
