using MediatR;
using Microsoft.AspNetCore.Mvc;
using ShopShare.Application.Roles.Commands.Create;
using ShopShare.Application.Roles.Commands.Delete;
using ShopShare.Application.Roles.Commands.Update;
using ShopShare.Application.Roles.Queries.GetAll;
using ShopShare.Application.Roles.Queries.GetOne;
using ShopShare.Application.Services.Mapper;
using ShopShare.Contracts.Roles;
using ShopShare.Domain.Common.Models;
using ShopShare.Domain.RoleAggregate;

namespace ShopShare.API.Controllers
{
    [Route("api/[controller]")]
    public class RolesController : ApiController
    {
        private readonly ISender _mediatr;
        private readonly IMapper<CreateRoleRequest, CreateRoleCommand> _createRoleCommandMapper;
        private readonly IMapper<UpdateRoleRequest, UpdateRoleCommand> _updateRoleCommand;
        private readonly IMapper<Role, RoleResponse> _roleResponseMapper;

        public RolesController(
            ISender mediatr,
            IMapperFactory mapperFactory)
        {
            _mediatr = mediatr;
            _createRoleCommandMapper = mapperFactory.GetMapper<CreateRoleRequest, CreateRoleCommand>();
            _updateRoleCommand = mapperFactory.GetMapper<UpdateRoleRequest, UpdateRoleCommand>();
            _roleResponseMapper = mapperFactory.GetMapper<Role, RoleResponse>();
        }

        [HttpGet]
        public async Task<ActionResult<Result<IEnumerable<RoleResponse>>>> GetAll()
        {
            var result = await _mediatr.Send(new GetRolesQuery());

            return result.IsSuccess
                ? Ok(
                    Result.Success(
                        _roleResponseMapper.Map(result.Value)))
                : NotFound(
                    result.ToResult());
        }

        [HttpGet("{id}", Name = nameof(GetRole))]
        public async Task<ActionResult<Result<RoleResponse>>> GetRole(Guid id)
        {
            var result = await _mediatr.Send(new GetRoleQuery(id));

            return result.IsSuccess
                ? Ok(
                    Result.Success(
                        _roleResponseMapper.Map(result.Value)))
                : NotFound(
                    result.ToResult());
        }

        [HttpPost]
        public async Task<ActionResult<Result<RoleResponse>>> Add(CreateRoleRequest request, CancellationToken cancellationToken = default)
        {
            var command = _createRoleCommandMapper.Map(request);
            var result = await _mediatr.Send(command, cancellationToken);

            return result.IsSuccess
                ? CreatedAtAction(
                    nameof(GetRole),
                    new { id = result.Value.Id.Value },
                    Result.Success(_roleResponseMapper.Map(result.Value)))
                : BadRequest(result.ToResult());
        }

        [HttpPut]
        public async Task<ActionResult<Result>> Update(UpdateRoleRequest request, CancellationToken cancellationToken = default)
        {
            var command = _updateRoleCommand.Map(request);
            var result = await _mediatr.Send(command, cancellationToken);

            return result.IsSuccess
                ? Ok(result)
                : BadRequest(result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Result>> Delete(Guid id)
        {
            var command = new DeleteRoleCommand(id);
            var result = await _mediatr.Send(command);

            return result.IsSuccess
                ? Ok(result)
                : BadRequest(result);
        }
    }
}
