using ShopShare.Application.ListItem.Commands.Delete;
using ShopShare.Application.Services.Mapper;
using ShopShare.Contracts.ListITemContracts;
using ShopShare.Domain.ShoppingListAggregate.ValueObjects;

namespace ShopShare.API.Common.Mappers
{
    public class DeleteListItemRequestToDeleteListItemCommandMapper : IMapper<DeleteListItemRequest, DeleteListItemCommand>
    {
        public DeleteListItemCommand Map(DeleteListItemRequest source)
        {
            return new DeleteListItemCommand(
                ShoppingListId.Create(source.ShoppingListId),
                ListItemId.Create(source.ListItemId));
        }

        public IEnumerable<DeleteListItemCommand> Map(IEnumerable<DeleteListItemRequest> source)
        {
            return source.Select(Map);
        }
    }
}
