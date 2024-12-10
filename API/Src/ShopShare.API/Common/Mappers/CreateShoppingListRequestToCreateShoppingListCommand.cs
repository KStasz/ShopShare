using ShopShare.Application.Services.Mapper;
using ShopShare.Application.ShoppingList.Commands.Create;
using ShopShare.Contracts.ShoppingListContracts;

namespace ShopShare.API.Common.Mappers
{
    public class CreateShoppingListRequestToCreateShoppingListCommand : IMapper<CreateShoppingListRequest, CreateShoppingListCommand>
    {
        public CreateShoppingListCommand Map(CreateShoppingListRequest source)
        {
            return new CreateShoppingListCommand(
                source.ListName,
                source.Description);
        }

        public IEnumerable<CreateShoppingListCommand> Map(IEnumerable<CreateShoppingListRequest> source)
        {
            return source.Select(Map);
        }
    }
}
