using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShopShare.Application.Authentication.Commands;
using ShopShare.Application.Authentication.Models;
using ShopShare.Application.Authentication.Query;
using ShopShare.Application.Services.Mapper;
using ShopShare.Contracts.Authentication;
using ShopShare.Domain.Common.Models;
using ShopShare.Infrastructure.Authentication;

namespace ShopShare.API.Controllers
{
    [Route("api/[controller]")]
    public class AuthenticationController : ApiController
    {
        private readonly ISender _mediator;
        private readonly IMapper<RegisterRequest, RegisterCommand> _registerCommandMapper;
        private readonly IMapper<LoginRequest, LoginQuery> _loginQueryMapper;
        private readonly JwtSettings _jwtSettings;
        private readonly IMapper<AuthenticationResult, AuthenticationResponse> _authenticationResponseMapper;
        private readonly ILogger<AuthenticationController> _logger;

        public AuthenticationController(
            ISender mediator,
            JwtSettings jwtSettings,
            IMapperFactory mapperFactory,
            ILogger<AuthenticationController> logger)
        {
            _mediator = mediator;
            _jwtSettings = jwtSettings;
            _registerCommandMapper = mapperFactory.GetMapper<RegisterRequest, RegisterCommand>();
            _loginQueryMapper = mapperFactory.GetMapper<LoginRequest, LoginQuery>();
            _authenticationResponseMapper = mapperFactory.GetMapper<AuthenticationResult, AuthenticationResponse>();
            _logger = logger;
        }

        [HttpPost("Login")]
        [AllowAnonymous]
        public async Task<ActionResult<Result<AuthenticationResponse>>> Login(LoginRequest loginRequest, bool storeInCookie = false, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("Trying to login user...");
            var query = _loginQueryMapper.Map(loginRequest);
            var result = await _mediator.Send(query);

            if (result.IsFailure)
            {
                _logger.LogError("Logging in user failed.");
                return BadRequest(result.Error);
            }

            if (storeInCookie)
            {
                _logger.LogInformation($"Storing JWT in cookie. Parameter: StoreInCookie -> {storeInCookie}");
                HttpContext.Response.Cookies.Append("Authorization", result.Value.token, new CookieOptions()
                {
                    Expires = DateTime.UtcNow.AddMinutes(_jwtSettings.ExpiryTime),
                    HttpOnly = true,
                    Secure = true
                });

                return Ok();
            }

            _logger.LogInformation("Logging in user succeeded. Returning JWT to user.");
            return Ok(
                _authenticationResponseMapper.Map(
                    result.Value));
        }

        [HttpPost("Logout")]
        public IActionResult Logout(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Deleting JWT from cookie.");
            HttpContext.Response.Cookies.Delete("Authorization");
            
            return Ok();
        }

        [HttpPost("Register")]
        [AllowAnonymous]
        public async Task<ActionResult<Result>> Register(RegisterRequest request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Trying to register new user.");
            var result = await _mediator.Send(_registerCommandMapper.Map(request));

            return result.IsSuccess
                ? Ok(result)
                : BadRequest(result);
        }
    }
}
