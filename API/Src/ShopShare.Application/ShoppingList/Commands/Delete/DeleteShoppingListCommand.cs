using MediatR;
using ShopShare.Domain.Common.Models;
using ShopShare.Domain.ShoppingListAggregate.ValueObjects;

namespace ShopShare.Application.ShoppingList.Commands.Delete
{
    public record DeleteShoppingListCommand(ShoppingListId Id) : IRequest<Result>;
}
