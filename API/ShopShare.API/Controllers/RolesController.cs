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
            IMapper<CreateRoleRequest, CreateRoleCommand> createRoleCommandMapper,
            IMapper<UpdateRoleRequest, UpdateRoleCommand> updateRoleCommand,
            IMapper<Role, RoleResponse> roleResponseMapper)
        {
            _mediatr = mediatr;
            _createRoleCommandMapper = createRoleCommandMapper;
            _updateRoleCommand = updateRoleCommand;
            _roleResponseMapper = roleResponseMapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _mediatr.Send(new GetRolesQuery());

            return result.IsSuccess
                ? Ok(_roleResponseMapper.Map(result.Value))
                : NotFound(result.ToResult());
        }

        [HttpGet("{id}", Name = nameof(Get))]
        public async Task<IActionResult> Get(Guid id)
        {
            var result = await _mediatr.Send(new GetRoleQuery(id));

            return result.IsSuccess
                ? Ok(_roleResponseMapper.Map(result.Value))
                : NotFound(result.ToResult());
        }

        [HttpPost]
        public async Task<IActionResult> Add(CreateRoleRequest request, CancellationToken cancellationToken = default)
        {
            var command = _createRoleCommandMapper.Map(request);
            var result = await _mediatr.Send(command, cancellationToken);

            return result.IsSuccess
                ? CreatedAtAction(
                    nameof(Get), 
                    new { id = result.Value.Id.Value }, 
                    _roleResponseMapper.Map(result.Value))
                : BadRequest(result.ToResult());
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateRoleRequest request, CancellationToken cancellationToken = default)
        {
            var command = _updateRoleCommand.Map(request);
            var result = await _mediatr.Send(command, cancellationToken);

            return result.IsSuccess
                ? Ok()
                : BadRequest(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var command = new DeleteRoleCommand(id);
            var result = await _mediatr.Send(command);

            return result.IsSuccess
                ? Ok()
                : BadRequest(result);
        }
    }
}
