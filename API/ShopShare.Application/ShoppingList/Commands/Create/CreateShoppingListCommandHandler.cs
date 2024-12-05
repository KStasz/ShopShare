using MediatR;
using ShopShare.Application.Services.Repositories;
using ShopShare.Domain.Common.Models;

using List = ShopShare.Domain.ShoppingListAggregate.ShoppingList;

namespace ShopShare.Application.ShoppingList.Commands.Create
{
    public class CreateShoppingListCommandHandler : IRequestHandler<CreateShoppingListCommand, Result<List>>
    {
        private readonly IShoppingListRepository _shoppingListRepository;

        public CreateShoppingListCommandHandler(
            IShoppingListRepository shoppingListRepository)
        {
            _shoppingListRepository = shoppingListRepository;
        }

        public async Task<Result<List>> Handle(CreateShoppingListCommand request, CancellationToken cancellationToken)
        {
            var list = List.CreateNew(
                request.ListName,
                request.Description);

            await _shoppingListRepository.AddAsync(list);

            return list;
        }
    }
}
