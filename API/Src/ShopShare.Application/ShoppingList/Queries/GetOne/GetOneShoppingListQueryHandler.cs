using MediatR;
using ShopShare.Application.Errors;
using ShopShare.Application.Services.Repositories;
using ShopShare.Domain.Common.Models;

namespace ShopShare.Application.ShoppingList.Queries.GetOne
{
    public class GetOneShoppingListQueryHandler : IRequestHandler<GetOneShoppingListQuery, Result<Domain.ShoppingListAggregate.ShoppingList>>
    {
        private readonly IShoppingListRepository _shoppingListRepository;

        public GetOneShoppingListQueryHandler(IShoppingListRepository shoppingListRepository)
        {
            _shoppingListRepository = shoppingListRepository;
        }

        public async Task<Result<Domain.ShoppingListAggregate.ShoppingList>> Handle(GetOneShoppingListQuery request, CancellationToken cancellationToken)
        {
            await Task.CompletedTask;
            var result = _shoppingListRepository.Get(x => x.Id == request.Id);

            if (result is null)
            {
                return Result.Failure<Domain.ShoppingListAggregate.ShoppingList>(
                    ApplicationErrors.ShoppingList.ShoppingListNotFound);
            }

            return result;
        }
    }
}
