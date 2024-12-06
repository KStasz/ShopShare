using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using ShopShare.Application.ListItem.Commands.Create;
using ShopShare.Application.ListItem.Commands.Delete;
using ShopShare.Application.ListItem.Queries;
using ShopShare.Application.Services.Mapper;
using ShopShare.Contracts.ListITemContracts;

namespace ShopShare.API.Hubs
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ListItemsHub : Hub
    {
        private readonly ILogger<ListItemsHub> _logger;
        private readonly ISender _mediatR;
        private readonly IMapper<CreateListItemRequest, CreateListItemCommand> _createListItemMapper;
        private readonly IMapper<DeleteListItemRequest, DeleteListItemCommand> _deleteListItemMapper;
        private readonly IMapper<GetListItemsRequest, GetListItemsQuery> _getListItemsQueryMapper;

        public ListItemsHub(
            ILogger<ListItemsHub> logger,
            IMapperFactory mapperFactory,
            ISender mediatR)
        {
            _logger = logger;
            _mediatR = mediatR;
            _createListItemMapper = mapperFactory.GetMapper<CreateListItemRequest, CreateListItemCommand>();
            _deleteListItemMapper = mapperFactory.GetMapper<DeleteListItemRequest, DeleteListItemCommand>();
            _getListItemsQueryMapper = mapperFactory.GetMapper<GetListItemsRequest, GetListItemsQuery>();
        }


        public async Task CreateListItem(CreateListItemRequest request)
        {
            _logger.LogInformation("Trying to create new list item...");
            var command = _createListItemMapper.Map(request);
            var result = await _mediatR.Send(command);
            _logger.LogInformation($"Result of creation: {result.IsSuccess}.");
        }

        public async Task DeleteListItem(DeleteListItemRequest request)
        {
            _logger.LogInformation($"Trying to remove list item with id: {request.ListItemId}.");
            var command = _deleteListItemMapper.Map(request);
            var result = await _mediatR.Send(command);
            _logger.LogInformation($"Result of removing: {result.IsSuccess}.");
        }

        public async Task GetAllItems(GetListItemsRequest request)
        {
            _logger.LogInformation($"Fetching items from list with id: {request.ShoppingListId}.");
            var query = _getListItemsQueryMapper.Map(request);
            var result = await _mediatR.Send(query);
            _logger.LogInformation($"Result of fetching: {result.IsSuccess}.");
            _logger.LogInformation($"Fetched {result.Value.Count()} rows.");
        }
    }
}
