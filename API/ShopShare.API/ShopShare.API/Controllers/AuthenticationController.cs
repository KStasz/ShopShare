using MediatR;
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

        public AuthenticationController(
            ISender mediator,
            IMapper<RegisterRequest, RegisterCommand> registerCommandMapper,
            IMapper<LoginRequest, LoginQuery> loginQueryMapper,
            JwtSettings jwtSettings)
        {
            _mediator = mediator;
            _registerCommandMapper = registerCommandMapper;
            _loginQueryMapper = loginQueryMapper;
            _jwtSettings = jwtSettings;
        }

        [HttpPost("Login")]
        public async Task<ActionResult<Result<AuthenticationResult>>> Login(LoginRequest loginRequest, bool storeInCookie = false, CancellationToken cancellationToken = default)
        {
            var query = _loginQueryMapper.Map(loginRequest);

            var result = await _mediator.Send(query);

            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }

            if (storeInCookie)
            {
                HttpContext.Response.Cookies.Append("Authorization", result.Value.token, new CookieOptions()
                {
                    Expires = DateTime.UtcNow.AddMinutes(_jwtSettings.ExpiryTime),
                    HttpOnly = true,
                    Secure = true
                });

                return Ok();
            }

            return Ok(result.Value);
        }

        [HttpPost("Logout")]
        public IActionResult Logout(CancellationToken cancellationToken)
        {
            HttpContext.Response.Cookies.Delete("Authorization");
            
            return Ok();
        }

        [HttpPost("Register")]
        public async Task<ActionResult<Result>> Register(RegisterRequest request, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(_registerCommandMapper.Map(request));

            return result.IsSuccess
                ? Ok()
                : BadRequest(result.Error);
        }
    }
}
