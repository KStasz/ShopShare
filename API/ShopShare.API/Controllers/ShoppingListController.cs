using Microsoft.AspNetCore.Mvc;

namespace ShopShare.API.Controllers
{
    [Route("api/[controller]")]
    public class ShoppingListController : ApiController
    {
        public ShoppingListController()
        {
            
        }

        [HttpPost]
        public IActionResult Create()
        {
            return Ok();
        }

        [HttpPut]
        public IActionResult Update()
        {
            return Ok();
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok();
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok();
        }

        [HttpDelete]
        public IActionResult Delete()
        {
            return Ok();
        }
    }
}
