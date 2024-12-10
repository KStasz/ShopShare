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
        private readonly IMapper<AddUserToRoleRequest, AddUserToRoleCommand> _addUserToRoleCommand;
        private readonly ISender _mediatR;
        private readonly ILogger<UsersController> _logger;

        public UsersController(
            IMapperFactory mapperFactory,
            ISender mediatR,
            ILogger<UsersController> logger)
        {
            _userResponseMapper = mapperFactory.GetMapper<User, UserResponse>();
            _updateUserCommandMapper = mapperFactory.GetMapper<UpdateUserRequest, UpdateUserCommand>();
            _addUserToRoleCommand = mapperFactory.GetMapper<AddUserToRoleRequest, AddUserToRoleCommand>();
            _mediatR = mediatR;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<Result<IEnumerable<UserResponse>>>> GetAll(CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("Fetching all users...");
            var command = new GetUsersQuery();
            var result = await _mediatR.Send(command, cancellationToken);

            if (!result.IsSuccess)
            {
                _logger.LogWarning("Failed to fetch users.");

                return BadRequest(result.ToResult());
            }

            _logger.LogInformation("Successfully fetched users");

            return Ok(_userResponseMapper.Map(result.Value));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Result<UserResponse>>> Get(Guid id, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation($"Fetching user with Id: {id}");
            var command = new GetUserQuery(UserId.Create(id));
            var result = await _mediatR.Send(command, cancellationToken);

            if (!result.IsSuccess)
            {
                _logger.LogWarning("Failed to fetch user.");

                return NotFound(result.ToResult());
            }

            _logger.LogInformation("Successfully fetched user.");

            return Ok(_userResponseMapper.Map(result.Value));
        }

        [HttpPut]
        public async Task<ActionResult<Result>> Update(UpdateUserRequest request, CancellationToken cancellationToken = default)
        {
            var command = _updateUserCommandMapper.Map(request);
            var result = await _mediatR.Send(command, cancellationToken);

            return result.IsSuccess
                ? Ok(result)
                : BadRequest(result);
        }

        [HttpPut("AddUserToRole")]
        public async Task<ActionResult<Result>> AddUserToRole(AddUserToRoleRequest request, CancellationToken cancellationToken = default)
        {
            var command = _addUserToRoleCommand.Map(request);
            var result = await _mediatR.Send(command, cancellationToken);

            return result.IsSuccess
                ? Ok(result)
                : BadRequest(result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Result>> Delete(Guid id, CancellationToken cancellationToken = default)
        {
            var command = new DeleteUserCommand(UserId.Create(id));
            var result = await _mediatR.Send(command, cancellationToken);

            return result.IsSuccess
                ? Ok(result)
                : NotFound(result);
        }
    }
}
