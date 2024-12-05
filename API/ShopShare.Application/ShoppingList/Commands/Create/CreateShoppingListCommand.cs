using MediatR;
using ShopShare.Domain.Common.Models;

using List = ShopShare.Domain.ShoppingListAggregate.ShoppingList;

namespace ShopShare.Application.ShoppingList.Commands.Create
{
    public record CreateShoppingListCommand(
        string ListName,
        string Description)
        : IRequest<Result<List>>;
}
