using MediatR;
using ShopShare.Domain.Common.Models;
using ShopShare.Domain.ShoppingListAggregate.ValueObjects;

namespace ShopShare.Application.ShoppingList.Commands.Update
{
    public record UpdateShoppingListCommand(
        ShoppingListId Id,
        string ListName,
        string Description) : IRequest<Result>;
}
