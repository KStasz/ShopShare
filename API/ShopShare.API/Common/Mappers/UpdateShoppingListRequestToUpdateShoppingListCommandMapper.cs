using ShopShare.Application.Services.Mapper;
using ShopShare.Application.ShoppingList.Commands.Update;
using ShopShare.Contracts.ShoppingListContracts;
using ShopShare.Domain.ShoppingListAggregate.ValueObjects;

namespace ShopShare.API.Common.Mappers
{
    public class UpdateShoppingListRequestToUpdateShoppingListCommandMapper : IMapper<UpdateShoppingListRequest, UpdateShoppingListCommand>
    {
        public UpdateShoppingListCommand Map(UpdateShoppingListRequest source)
        {
            return new UpdateShoppingListCommand(
                ShoppingListId.Create(source.Id),
                source.ListName,
                source.Description);
        }

        public IEnumerable<UpdateShoppingListCommand> Map(IEnumerable<UpdateShoppingListRequest> source)
        {
            return source.Select(Map);
        }
    }
}
