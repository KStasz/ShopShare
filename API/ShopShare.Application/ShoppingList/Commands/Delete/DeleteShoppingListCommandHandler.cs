using MediatR;
using ShopShare.Application.Errors;
using ShopShare.Application.Services.Repositories;
using ShopShare.Domain.Common.Models;

namespace ShopShare.Application.ShoppingList.Commands.Delete
{
    public class DeleteShoppingListCommandHandler : IRequestHandler<DeleteShoppingListCommand, Result>
    {
        private readonly IShoppingListRepository _repository;

        public DeleteShoppingListCommandHandler(IShoppingListRepository repository)
        {
            _repository = repository;
        }

        public async Task<Result> Handle(DeleteShoppingListCommand request, CancellationToken cancellationToken)
        {
            var listResult = _repository.Get(x => x.Id == request.Id);

            if (listResult is null)
            {
                return Result.Failure(ApplicationErrors.ShoppingList.ShoppingListNotFound);
            }

            await _repository.DeleteAsync(listResult);

            return Result.Success();
        }
    }
}
