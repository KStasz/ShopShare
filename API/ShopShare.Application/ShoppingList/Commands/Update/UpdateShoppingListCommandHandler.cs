using MediatR;
using ShopShare.Application.Errors;
using ShopShare.Application.Services.Repositories;
using ShopShare.Domain.Common.Models;

namespace ShopShare.Application.ShoppingList.Commands.Update
{
    public class UpdateShoppingListCommandHandler : IRequestHandler<UpdateShoppingListCommand, Result>
    {
        private readonly IShoppingListRepository _shoppingListRepository;

        public UpdateShoppingListCommandHandler(IShoppingListRepository shoppingListRepository)
        {
            _shoppingListRepository = shoppingListRepository;
        }

        public async Task<Result> Handle(UpdateShoppingListCommand request, CancellationToken cancellationToken)
        {
            var listResult = _shoppingListRepository.Get(x => x.Id == request.Id);

            if (listResult is null)
            {
                return Result.Failure(ApplicationErrors.ShoppingList.ShoppingListNotFound);
            }

            listResult.ChangeName(request.ListName);
            listResult.ChangeDescription(request.Description);

            await _shoppingListRepository.UpdateAsync(listResult);

            return Result.Success();
        }
    }
}
