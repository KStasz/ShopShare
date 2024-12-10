using MediatR;
using ShopShare.Domain.Common.Models;
using ShopShare.Domain.ShoppingListAggregate.ValueObjects;

namespace ShopShare.Application.ListItem.Commands.Delete
{
    public record DeleteListItemCommand(
        ShoppingListId ListId,
        ListItemId ItemId)
        : IRequest<Result>;
}
