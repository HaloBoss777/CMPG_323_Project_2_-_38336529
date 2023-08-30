using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project2_CMPG323.API.Filter;
using Project2_CMPG323.CORE.DTO;
using Project2_CMPG323.CORE.Services;

namespace Project2_CMPG323.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        //Implement the Order Service (Dependency Ingection)
        public readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            //Injects only the Dependencys that are needed
            _orderService = orderService;
        }


        //Get All Orders
        //https://Localhost:1234/api/Orders/Get/All
        [HttpGet]
        [Route("/api/Orders/Get/All")]
        [AuthorizationFilter]
        public async Task<IActionResult> GetAllOrders()
        {
            return Ok(await _orderService.GetAllOrdersAsync());
        }

        //Get a Specific Order
        //https://Localhost:1234/api/Orders/Get/{id}
        [HttpGet]
        [Route("/api/Orders/Get/{id}")]
        [AuthorizationFilter]
        public async Task<IActionResult> GetOrder([FromRoute] short id)
        {
            var foundRecord = await _orderService.GetOrderAsync(id);

            if (foundRecord is null) 
            {
                return NotFound();
            }

            return Ok(foundRecord);
        }

        //Create a Order
        //https://Localhost:1234/api/Orders/CreateOrder
        [HttpPost]
        [Route("/api/Orders/CreateOrder")]
        [AuthorizationFilter]
        public async Task<IActionResult> CreateOrder([FromBody] CreateOrderDTO createOrderDTO)
        {
            var CreatedOrder = await _orderService.CreateOrderAsync(createOrderDTO);

            return CreatedAtAction(nameof(GetOrder), new { id = CreatedOrder.OrderId }, CreatedOrder);
        }

        //Update a Order Record
        //https://Localhost:1234/api/Orders/UpdateOrder/{id}
        [HttpPatch]
        [Route("/api/Orders/UpdateOrder/{id}")]
        [AuthorizationFilter]
        public async Task<IActionResult> UpdateOrder([FromRoute]short id, [FromBody] UpdateOrderDTO updateOrderDTO)
        {
            var updatedRecord = await _orderService.UpdateOrderAsync(id, updateOrderDTO);

            if(updatedRecord is null)
            {
                return NotFound();
            }

            return Ok(updatedRecord);
        }

        //Delete a Order Record
        //https://Localhost:1234/api/Orders/DeleteOrder/{id}
        [HttpDelete]
        [Route("/api/Orders/DeleteOrder/{id}")]
        [AuthorizationFilter]
        public async Task<IActionResult> DeleteOrder([FromRoute] short id)
        {
            var deletedRecord = await _orderService.DeleteOrderAsync(id);

            if(deletedRecord is null)
            { 
                return NotFound();
            }

            return Ok(deletedRecord);
        }

        //Get All Orders of a Customer
        //https://localhost:1234/api/Order/Get/CustomerOrders/{id}
        [HttpGet]
        [Route("/api/Order/Get/CustomerOrders/{id}")]
        [AuthorizationFilter]
        public async Task<IActionResult> GetAllCustomerOrders([FromRoute] short id)
        {
            return Ok(await _orderService.GetAllOrdersOfCustomerAsync(id));
        }
    }
}
