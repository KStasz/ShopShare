using MediatR;
using ShopShare.Domain.Common.Models;

namespace ShopShare.Application.ShoppingList.Queries.GetAll
{
    public record GetAllShoppingListsQuery()
        : IRequest<Result<IEnumerable<Domain.ShoppingListAggregate.ShoppingList>>>;
}
