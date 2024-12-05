using MediatR;
using ShopShare.Domain.Common.Models;
using ShopShare.Domain.ShoppingListAggregate.ValueObjects;

namespace ShopShare.Application.ShoppingList.Queries.GetOne
{
    public record GetOneShoppingListQuery(ShoppingListId Id) 
        : IRequest<Result<Domain.ShoppingListAggregate.ShoppingList>>;
}
