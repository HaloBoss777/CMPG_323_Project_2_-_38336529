using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project2_CMPG323.CORE.DTO;
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

        //https://Localhost:1234/api/Orders/Get/{id}
        [HttpGet]
        [Route("/api/Orders/Get/{id}")]
        public async Task<IActionResult> GetOrder([FromRoute] short id)
        {
            var foundRecord = await _orderService.GetOrderAsync(id);

            if (foundRecord is null) 
            {
                return NotFound();
            }

            return Ok(foundRecord);
        }

        //https://Localhost:1234/api/Orders/CreateOrder
        [HttpPost]
        [Route("/api/Orders/CreateOrder")]
        public async Task<IActionResult> CreateOrder([FromBody] CreateOrderDTO createOrderDTO)
        {
            var CreatedOrder = await _orderService.CreateOrderAsync(createOrderDTO);

            return CreatedAtAction(nameof(GetOrder), new { id = CreatedOrder.OrderId }, CreatedOrder);
        }

        //https://Localhost:1234/api/Orders/UpdateOrder/{id}
        [HttpPatch]
        [Route("/api/Orders/UpdateOrder/{id}")]
        public async Task<IActionResult> UpdateOrder([FromRoute]short id, [FromBody] UpdateOrderDTO updateOrderDTO)
        {
            var updatedRecord = await _orderService.UpdateOrderAsync(id, updateOrderDTO);

            if(updatedRecord is null)
            {
                return NotFound();
            }

            return Ok(updatedRecord);
        }
    }
}
