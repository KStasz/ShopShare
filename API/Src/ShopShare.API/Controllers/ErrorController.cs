using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ShopShare.Domain.Common.Models;
using System.Net;

namespace ShopShare.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ErrorController : ControllerBase
    {
        [HttpGet]
        [HttpPost]
        [HttpPut]
        [HttpDelete]
        public IActionResult HandleError()
        {
            var context = HttpContext.Features.Get<IExceptionHandlerFeature>();
            var exception = context?.Error;

            var statusCode = exception switch
            {
                ArgumentNullException => HttpStatusCode.BadRequest,
                _ => HttpStatusCode.InternalServerError
            };

            var response = Result.Failure(
                new Error(
                    statusCode.ToString(),
                    exception?.Message ?? "An unexpected error occured."));

            return StatusCode((int)statusCode, response);
        }
    }
}
