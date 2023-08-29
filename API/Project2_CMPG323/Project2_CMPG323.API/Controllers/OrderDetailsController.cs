using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project2_CMPG323.CORE.Services;

namespace Project2_CMPG323.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderDetailsController : ControllerBase
    {
        private readonly IOrderDetails _orderDetailsSerive;

        public OrderDetailsController(IOrderDetails orderDetailsSerive)
        {
            _orderDetailsSerive = orderDetailsSerive;
        }

        //https://Localhost:1234/api/OrderDetails/Get/All
        [HttpGet]
        [Route("/api/OrderDetails/Get/All")]
        public async Task<IActionResult> GetAllOrderDetails()
        {
            return Ok(await _orderDetailsSerive.GetAllOrderDetailsAsync());
        }

        //https://Localhost:1234/api/OrderDetails/Get/{id}
        [HttpGet]
        [Route("/api/OrderDetails/Get/{id}")]
        public async Task<IActionResult> GetOrderDetail([FromRoute] short id)
        {
            var foundRecord = await _orderDetailsSerive.GetOrderDetailAsync(id);

            if (foundRecord is null) 
            {
                return NotFound();
            }

            return Ok(foundRecord);
        }
    }
}
