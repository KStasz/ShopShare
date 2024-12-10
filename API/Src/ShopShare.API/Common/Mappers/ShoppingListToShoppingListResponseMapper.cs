using ShopShare.Application.Services.Mapper;
using ShopShare.Contracts.ShoppingListContracts;
using ShopShare.Domain.ShoppingListAggregate;
using ShopShare.Domain.ShoppingListAggregate.Entities;

namespace ShopShare.API.Common.Mappers
{
    public class ShoppingListToShoppingListResponseMapper : IMapper<ShoppingList, ShoppingListResponse>
    {
        private readonly IMapper<ListItem, ListItemResponse> _listItemResponseMapper;

        public ShoppingListToShoppingListResponseMapper(
            IMapperFactory mapperFactory)
        {
            _listItemResponseMapper = mapperFactory.GetMapper<ListItem, ListItemResponse>();
        }

        public ShoppingListResponse Map(ShoppingList source)
        {
            return new ShoppingListResponse(
                source.Id.Value,
                source.ListName,
                source.Description,
                source.CreationDate,
                source.UpdatedDate,
                _listItemResponseMapper.Map(source.Items).ToList());
        }

        public IEnumerable<ShoppingListResponse> Map(IEnumerable<ShoppingList> source)
        {
            return source.Select(Map);
        }
    }
}
