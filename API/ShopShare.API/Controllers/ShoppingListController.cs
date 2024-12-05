using MediatR;
using Microsoft.AspNetCore.Mvc;
using ShopShare.Application.Services.Mapper;
using ShopShare.Application.ShoppingList.Commands.Create;
using ShopShare.Application.ShoppingList.Commands.Delete;
using ShopShare.Application.ShoppingList.Commands.Update;
using ShopShare.Application.ShoppingList.Queries.GetAll;
using ShopShare.Application.ShoppingList.Queries.GetOne;
using ShopShare.Contracts.ShoppingListContracts;
using ShopShare.Domain.Common.Models;
using ShopShare.Domain.ShoppingListAggregate;
using ShopShare.Domain.ShoppingListAggregate.ValueObjects;

namespace ShopShare.API.Controllers
{
    [Route("api/[controller]")]
    public class ShoppingListController : ApiController
    {
        private readonly IMapper<CreateShoppingListRequest, CreateShoppingListCommand> _createShoppingListMapper;
        private readonly IMapper<UpdateShoppingListRequest, UpdateShoppingListCommand> _updateShoppingListMapper;
        private readonly IMapper<ShoppingList, ShoppingListResponse> _shoppingListResponseMapper;
        private readonly ISender _mediatR;

        public ShoppingListController(
            IMapperFactory mapperFactory,
            ISender mediatR)
        {
            _createShoppingListMapper = mapperFactory.GetMapper<CreateShoppingListRequest, CreateShoppingListCommand>();
            _updateShoppingListMapper = mapperFactory.GetMapper<UpdateShoppingListRequest, UpdateShoppingListCommand>();
            _shoppingListResponseMapper = mapperFactory.GetMapper<ShoppingList, ShoppingListResponse>();
            _mediatR = mediatR;
        }

        [HttpPost]
        public async Task<ActionResult<Result<ShoppingListResponse>>> Create(CreateShoppingListRequest request)
        {
            var command = _createShoppingListMapper.Map(request);
            var result = await _mediatR.Send(command);

            return result.IsSuccess
                ? CreatedAtAction(
                    nameof(GetShoppingList),
                    new { id = result.Value.Id },
                    Result.Success(_shoppingListResponseMapper.Map(result.Value)))
                : BadRequest(result.ToResult());
        }

        [HttpPut]
        public async Task<ActionResult<Result>> Update(UpdateShoppingListRequest updateShoppingListRequest)
        {
            var command = _updateShoppingListMapper.Map(updateShoppingListRequest);
            var result = await _mediatR.Send(command);

            return result.IsSuccess
                ? Ok(result)
                : NotFound(result);
        }

        [HttpGet("{id}", Name = nameof(GetShoppingList))]
        public async Task<ActionResult<Result<ShoppingListResponse>>> GetShoppingList(Guid id)
        {
            var query = new GetOneShoppingListQuery(
                ShoppingListId.Create(id));

            var result = await _mediatR.Send(query);

            return result.IsSuccess
                ? Ok(Result.Success(_shoppingListResponseMapper.Map(result.Value)))
                : BadRequest(result.ToResult());
        }

        [HttpGet]
        public async Task<ActionResult<Result<IEnumerable<ShoppingListResponse>>>> GetAll()
        {
            var query = new GetAllShoppingListsQuery();
            var result = await _mediatR.Send(query);
            
            return Ok(Result.Success(_shoppingListResponseMapper.Map(result.Value)));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Result>> Delete(Guid id)
        {
            var command = new DeleteShoppingListCommand(
                ShoppingListId.Create(id));
            var result = await _mediatR.Send(command);

            return result.IsSuccess
                ? Ok(result)
                : NotFound(result);
        }
    }
}
