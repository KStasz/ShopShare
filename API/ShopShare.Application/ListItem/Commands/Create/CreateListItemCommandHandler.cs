using MediatR;
using ShopShare.Application.Errors;
using ShopShare.Application.Services.Repositories;
using ShopShare.Domain.Common.Models;

namespace ShopShare.Application.ListItem.Commands.Create
{
    public class CreateListItemCommandHandler : IRequestHandler<CreateListItemCommand, Result<Domain.ShoppingListAggregate.Entities.ListItem>>
    {
        private readonly IShoppingListRepository _shopListRepository;
        public CreateListItemCommandHandler(IShoppingListRepository shopListRepository)
        {
            _shopListRepository = shopListRepository;
        }

        public async Task<Result<Domain.ShoppingListAggregate.Entities.ListItem>> Handle(CreateListItemCommand request, CancellationToken cancellationToken)
        {
            var shoppingList = _shopListRepository.Get(x => x.Id == request.ShoppingListId);

            if (shoppingList is null)
            {
                return Result.Failure<Domain.ShoppingListAggregate.Entities.ListItem>(
                    ApplicationErrors.ShoppingList.ShoppingListNotFound);
            }

            shoppingList.AddItem(request.ListItem);
            await _shopListRepository.UpdateAsync(shoppingList);

            return request.ListItem;
        }
    }
}
