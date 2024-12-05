using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using ShopShare.Domain.Common.Models;

namespace ShopShare.API.Controllers
{
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ApiController : ControllerBase
    {
        public override OkObjectResult Ok([ActionResultObjectValue] object? value)
        {
            if (value?.GetType().BaseType == typeof(Result))
            {
                return base.Ok(value);
            }

            return base.Ok(Result.Success(value));
        }
    }
}
