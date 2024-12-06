using ShopShare.Application.ListItem.Commands.Create;
using ShopShare.Application.Services.Mapper;
using ShopShare.Contracts.ListITemContracts;
using ShopShare.Domain.ShoppingListAggregate.Entities;
using ShopShare.Domain.ShoppingListAggregate.ValueObjects;

namespace ShopShare.API.Common.Mappers
{
    public class CreateListItemRequestToCreateListItemCommandMapper : IMapper<CreateListItemRequest, CreateListItemCommand>
    {
        public CreateListItemCommand Map(CreateListItemRequest source)
        {
            return new CreateListItemCommand(
                ShoppingListId.Create(source.ShoppingListId),
                ListItem.CreateNew(source.ItemName));
        }

        public IEnumerable<CreateListItemCommand> Map(IEnumerable<CreateListItemRequest> source)
        {
            return source.Select(Map);
        }
    }
}
