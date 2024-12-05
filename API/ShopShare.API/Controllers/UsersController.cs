using MediatR;
using Microsoft.AspNetCore.Mvc;
using ShopShare.Application.Services.Mapper;
using ShopShare.Application.Users.Commands.AddUserToRole;
using ShopShare.Application.Users.Commands.Delete;
using ShopShare.Application.Users.Commands.Update;
using ShopShare.Application.Users.Queries.GetAll;
using ShopShare.Application.Users.Queries.GetOne;
using ShopShare.Contracts.Users;
using ShopShare.Domain.Common.Models;
using ShopShare.Domain.UserAggregate;
using ShopShare.Domain.UserAggregate.ValueObjects;

namespace ShopShare.API.Controllers
{
    [Route("api/[controller]")]
    public class UsersController : ApiController
    {
        private readonly IMapper<User, UserResponse> _userResponseMapper;
        private readonly IMapper<UpdateUserRequest, UpdateUserCommand> _updateUserCommandMapper;
        private readonly ISender _mediatR;
        private readonly IMapper<AddUserToRoleRequest, AddUserToRoleCommand> _addUserToRoleCommand;

        public UsersController(
            IMapper<User, UserResponse> userResponseMapper,
            IMapper<UpdateUserRequest, UpdateUserCommand> updateUserCommandMapper,
            ISender mediatR,
            IMapper<AddUserToRoleRequest, AddUserToRoleCommand> addUserToRoleCommand)
        {
            _userResponseMapper = userResponseMapper;
            _updateUserCommandMapper = updateUserCommandMapper;
            _mediatR = mediatR;
            _addUserToRoleCommand = addUserToRoleCommand;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken = default)
        {
            var command = new GetUsersQuery();
            var result = await _mediatR.Send(command, cancellationToken);

            if (!result.IsSuccess)
            {
                return BadRequest(result.ToResult());
            }

            return Ok(
                Result.Success(
                    _userResponseMapper.Map(result.Value)));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id, CancellationToken cancellationToken = default)
        {
            var command = new GetUserQuery(UserId.Create(id));
            var result = await _mediatR.Send(command, cancellationToken);

            if (!result.IsSuccess)
            {
                return NotFound(result.ToResult());
            }

            return Ok(
                Result.Success(
                    _userResponseMapper.Map(result.Value)));
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateUserRequest request, CancellationToken cancellationToken = default)
        {
            var command = _updateUserCommandMapper.Map(request);
            var result = await _mediatR.Send(command, cancellationToken);

            return result.IsSuccess
                ? Ok()
                : BadRequest(result);
        }

        [HttpPut("AddUserToRole")]
        public async Task<IActionResult> AddUserToRole(AddUserToRoleRequest request, CancellationToken cancellationToken = default)
        {
            var command = _addUserToRoleCommand.Map(request);
            var result = await _mediatR.Send(command, cancellationToken);

            return result.IsSuccess
                ? Ok(result)
                : BadRequest(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken = default)
        {
            var command = new DeleteUserCommand(UserId.Create(id));
            var result = await _mediatR.Send(command, cancellationToken);

            return result.IsSuccess
                ? Ok(result)
                : NotFound(result);
        }
    }
}
