using ShopShare.Application.Services.Mapper;
using ShopShare.Contracts.ShoppingListContracts;
using ShopShare.Domain.ShoppingListAggregate.Entities;

namespace ShopShare.API.Common.Mappers
{
    public class ListItemToListItemResponseMapper : IMapper<ListItem, ListItemResponse>
    {
        public ListItemResponse Map(ListItem source)
        {
            return new ListItemResponse(
                source.Id.Value,
                source.ItemName,
                source.CreationDate);
        }

        public IEnumerable<ListItemResponse> Map(IEnumerable<ListItem> source)
        {
            return source.Select(Map);
        }
    }
}
