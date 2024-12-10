using MediatR;
using ShopShare.Application.Errors;
using ShopShare.Application.Services.Repositories;
using ShopShare.Domain.Common.Models;

namespace ShopShare.Application.ListItem.Commands.Delete
{
    internal class DeleteListItemCommandHandler : IRequestHandler<DeleteListItemCommand, Result>
    {
        public readonly IShoppingListRepository _shoppingListRepository;

        public DeleteListItemCommandHandler(IShoppingListRepository shoppingListRepository)
        {
            _shoppingListRepository = shoppingListRepository;
        }

        public async Task<Result> Handle(DeleteListItemCommand request, CancellationToken cancellationToken)
        {
            var shoppingList = _shoppingListRepository.Get(x => x.Id == request.ListId);

            if (shoppingList is null)
            {
                return Result.Failure(ApplicationErrors.ShoppingList.ShoppingListNotFound);
            }

            var listItem = shoppingList.Items.FirstOrDefault(x => x.Id == request.ItemId);

            if (listItem is null)
            {
                return Result.Failure(ApplicationErrors.ListItem.ListItemNotFound);
            }

            shoppingList.RemoveItem(listItem);

            await _shoppingListRepository.UpdateAsync(shoppingList);

            return Result.Success();
        }
    }
}
