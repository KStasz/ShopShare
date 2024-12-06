using MediatR;
using ShopShare.Application.Errors;
using ShopShare.Application.Services.Repositories;
using ShopShare.Domain.Common.Models;

namespace ShopShare.Application.ListItem.Queries
{
    public class GetListItemsQueryHandler : IRequestHandler<GetListItemsQuery, Result<IEnumerable<Domain.ShoppingListAggregate.Entities.ListItem>>>
    {
        private readonly IShoppingListRepository _shoppingListRepository;

        public GetListItemsQueryHandler(IShoppingListRepository shoppingListRepository)
        {
            _shoppingListRepository = shoppingListRepository;
        }

        public async Task<Result<IEnumerable<Domain.ShoppingListAggregate.Entities.ListItem>>> Handle(GetListItemsQuery request, CancellationToken cancellationToken)
        {
            await Task.CompletedTask;
            var shoppingList = _shoppingListRepository.Get(x => x.Id == request.ShoppingListId);

            if (shoppingList is null)
            {
                return Result.Failure<IEnumerable<Domain.ShoppingListAggregate.Entities.ListItem>>(
                    ApplicationErrors.ShoppingList.ShoppingListNotFound);
            }

            return Result.Success(shoppingList.Items.AsEnumerable());
        }
    }
}
