using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project2_CMPG323.API.Filter;
using Project2_CMPG323.CORE.DTO;
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
        [AuthorizationFilter]
        public async Task<IActionResult> GetAllOrderDetails()
        {
            return Ok(await _orderDetailsSerive.GetAllOrderDetailsAsync());
        }

        //https://Localhost:1234/api/OrderDetails/Get/{id}
        [HttpGet]
        [Route("/api/OrderDetails/Get/{id}")]
        [AuthorizationFilter]
        public async Task<IActionResult> GetOrderDetail([FromRoute] short id)
        {
            var foundRecord = await _orderDetailsSerive.GetOrderDetailAsync(id);

            if (foundRecord is null) 
            {
                return NotFound();
            }

            return Ok(foundRecord);
        }


        //https://Localhost:1234/api/OderDetails/CreateOrderDetail
        [HttpPost]
        [Route("/api/OderDetails/CreateOrderDetail")]
        [AuthorizationFilter]
        public async Task<IActionResult> CreateOrderDetail([FromBody] CreateOrderDetailDTO createOrderDetailDTO)
        {
            var createdOrderDetail = await _orderDetailsSerive.CreateOrderDetailAsync(createOrderDetailDTO);

            return CreatedAtAction(nameof(GetOrderDetail), new {id = createdOrderDetail.OrderDetailsId} ,createdOrderDetail);
        }

        //https://Localhost:1234/api/OderDetails/UpdateOrderDetail
        [HttpPatch]
        [Route("/api/OderDetails/UpdateOrderDetail/{id}")]
        [AuthorizationFilter]
        public async Task<IActionResult> UpdateOrderDetail([FromRoute] short id, [FromBody] UpdateOrderDetailDTO updateOrderDetailDTO)
        {
            var updatedRecord = await _orderDetailsSerive.UpdateOrderDetailAsync(id, updateOrderDetailDTO);

            if(updatedRecord is null)
            {
                return NotFound();
            }

            return Ok(updatedRecord);
        }

        //https://Localhost:1234/api/OderDetails/DeleteOrderDetail/{id}
        [HttpDelete]
        [Route("/api/OderDetails/DeleteOrderDetail/{id}")]
        [AuthorizationFilter]
        public async Task<IActionResult> DeleteOrderDetail([FromRoute] short id)
        {
            var deletedRecord = await _orderDetailsSerive.DeleteOrderDetailAsync(id);

            if(deletedRecord is null)
            {
                return NotFound();
            }

            return Ok(deletedRecord);
        }

        //https://Localhost:1234/api/OderDetails/GetAllProducts/{id}
        [HttpGet]
        [Route("/api/OderDetails/GetAllProducts/{id}")]
        [AuthorizationFilter]
        public async Task<IActionResult> GetAllProductsOfOrder([FromRoute] short id)
        {
            var foundRecords = await _orderDetailsSerive.GetAllProductsOfOrderAsync(id);

            if(foundRecords is null)
            {
                return NotFound();
            }

            return Ok(foundRecords);
        }
    }
}
