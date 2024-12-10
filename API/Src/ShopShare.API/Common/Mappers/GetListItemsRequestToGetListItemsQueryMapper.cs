using ShopShare.Application.ListItem.Queries;
using ShopShare.Application.Services.Mapper;
using ShopShare.Contracts.ListITemContracts;
using ShopShare.Domain.ShoppingListAggregate.ValueObjects;

namespace ShopShare.API.Common.Mappers
{
    public class GetListItemsRequestToGetListItemsQueryMapper : IMapper<GetListItemsRequest, GetListItemsQuery>
    {
        public GetListItemsQuery Map(GetListItemsRequest source)
        {
            return new GetListItemsQuery(
                ShoppingListId.Create(source.ShoppingListId));
        }

        public IEnumerable<GetListItemsQuery> Map(IEnumerable<GetListItemsRequest> source)
        {
            return source.Select(Map);
        }
    }
}
