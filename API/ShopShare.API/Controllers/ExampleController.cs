using Microsoft.AspNetCore.Mvc;

namespace ShopShare.API.Controllers
{
    [Route("api/[controller]")]
    public class ExampleController : ApiController
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok();
        }

    }
}
