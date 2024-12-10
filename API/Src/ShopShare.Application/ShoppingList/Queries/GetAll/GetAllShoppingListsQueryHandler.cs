using MediatR;
using ShopShare.Application.Services.Repositories;
using ShopShare.Domain.Common.Models;

namespace ShopShare.Application.ShoppingList.Queries.GetAll
{
    public class GetAllShoppingListsQueryHandler : IRequestHandler<GetAllShoppingListsQuery, Result<IEnumerable<Domain.ShoppingListAggregate.ShoppingList>>>
    {
        private readonly IShoppingListRepository _repository;

        public GetAllShoppingListsQueryHandler(IShoppingListRepository repository)
        {
            _repository = repository;
        }

        public async Task<Result<IEnumerable<Domain.ShoppingListAggregate.ShoppingList>>> Handle(GetAllShoppingListsQuery request, CancellationToken cancellationToken)
        {
            await Task.CompletedTask;
            var result = _repository.GetAll(x => true);

            return Result.Success(result);
        }
    }
}
