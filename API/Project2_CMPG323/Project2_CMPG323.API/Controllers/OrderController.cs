using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project2_CMPG323.CORE.Services;

namespace Project2_CMPG323.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        public readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }


        //https://Localhost:1234/api/Orders/Get/All
        [HttpGet]
        [Route("/api/Orders/Get/All")]
        public async Task<IActionResult> GetAllOrders()
        {
            return Ok(await _orderService.GetAllOrdersAsync());
        }


    }
}
